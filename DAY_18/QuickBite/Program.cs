using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QuickBite
{
    // ==========================================
    // 1. DOMAIN MODEL & INTERFACES
    // ==========================================

    /// <summary>
    /// Base interface for any domain model with an integer identity.
    /// Allows the Repository class to enforce type constraints.
    /// </summary>
    public interface IEntity
    {
        int Id { get; }
    }

    /// <summary>
    /// Lifecycle states for an Order.
    /// </summary>
    public enum OrderStatus
    {
        Placed,
        Queued,
        Dispatched,
        Delivered,
        Cancelled
    }

    public class MenuItem : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public MenuItem(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }

    public class Restaurant : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public List<MenuItem> Menu { get; set; } = new List<MenuItem>();

        public Restaurant(int id, string name, bool isOpen = true)
        {
            Id = id;
            Name = name;
            IsOpen = isOpen;
        }
    }

    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsVip { get; set; }

        public Customer(int id, string name, bool isVip = false)
        {
            Id = id;
            Name = name;
            IsVip = isVip;
        }
    }

    public class OrderItem
    {
        public MenuItem MenuItem { get; set; }
        public int Quantity { get; set; }

        public OrderItem(MenuItem menuItem, int quantity)
        {
            MenuItem = menuItem;
            Quantity = quantity;
        }
    }

    public class Order : IEntity
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public DateTime PlacedAt { get; set; }
        public bool IsExpress { get; set; }
        public OrderStatus Status { get; set; }

        public Order(int id, Customer customer, Restaurant restaurant, bool isExpress = false)
        {
            Id = id;
            Customer = customer;
            Restaurant = restaurant;
            PlacedAt = DateTime.Now;
            IsExpress = isExpress;
            Status = OrderStatus.Placed;
        }
    }

    public class DeliveryAgent : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DeliveryAgent(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    /// <summary>
    /// Tracks dispatch events so they can be popped from a Stack and undone.
    /// </summary>
    public class DispatchRecord
    {
        public Order Order { get; set; }
        public DeliveryAgent Agent { get; set; }
        public DateTime DispatchedAt { get; set; }

        public DispatchRecord(Order order, DeliveryAgent agent)
        {
            Order = order;
            Agent = agent;
            DispatchedAt = DateTime.Now;
        }
    }

    // ==========================================
    // 2. GENERIC REPOSITORY LAYER
    // ==========================================

    /// <summary>
    /// A generic repository backed by a Dictionary<int, T> for O(1) lookups.
    /// Implements IEnumerable<T> so callers can directly use `foreach` on repository instances.
    /// </summary>
    public class Repository<T> : IEnumerable<T> where T : class, IEntity
    {
        // Internal storage mapping ID -> Entity
        private readonly Dictionary<int, T> _storage = new Dictionary<int, T>();

        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _storage[entity.Id] = entity;
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (!_storage.ContainsKey(entity.Id))
                throw new KeyNotFoundException($"Entity with ID {entity.Id} not found.");
            
            _storage[entity.Id] = entity;
        }

        public bool Remove(int id)
        {
            return _storage.Remove(id);
        }

        public T GetById(int id)
        {
            _storage.TryGetValue(id, out T entity);
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _storage.Values;
        }

        // Implementation of IEnumerable<T> allows direct iteration
        public IEnumerator<T> GetEnumerator()
        {
            return _storage.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    // ==========================================
    // 3. COMPARER & DISPATCH QUEUE
    // ==========================================

    /// <summary>
    /// Custom comparer prioritizing:
    /// 1. Express orders over Non-Express
    /// 2. VIP customers over Regular customers
    /// 3. Earlier PlacedAt timestamps (FIFO)
    /// </summary>
    public class OrderPriorityComparer : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            // 1. Express priority (High priority first)
            if (x.IsExpress != y.IsExpress)
                return y.IsExpress.CompareTo(x.IsExpress);

            // 2. VIP priority (High priority first)
            bool xVip = x.Customer?.IsVip ?? false;
            bool yVip = y.Customer?.IsVip ?? false;
            if (xVip != yVip)
                return yVip.CompareTo(xVip);

            // 3. Earliest timestamp (FIFO logic: smaller DateTime value comes first)
            int timeComparison = x.PlacedAt.CompareTo(y.PlacedAt);
            if (timeComparison != 0)
                return timeComparison;

            // Fallback to Unique ID to avoid duplicate keys in binary trees/sets
            return x.Id.CompareTo(y.Id);
        }
    }

    /// <summary>
    /// Multi-queue dispatch engine maintaining distinct tiers to achieve high priority dispatch 
    /// while guaranteeing O(1) enqueue and dequeue strict FIFO fairness per priority level.
    /// </summary>
    public class DispatchQueue
    {
        // Tier 1: Express or VIP orders
        private readonly Queue<Order> _priorityQueue = new Queue<Order>();
        // Tier 2: Normal orders
        private readonly Queue<Order> _normalQueue = new Queue<Order>();

        public void Enqueue(Order order)
        {
            order.Status = OrderStatus.Queued;
            
            // Tier condition: Either order is express OR customer is VIP
            if (order.IsExpress || (order.Customer != null && order.Customer.IsVip))
            {
                _priorityQueue.Enqueue(order);
            }
            else
            {
                _normalQueue.Enqueue(order);
            }
        }

        public Order DispatchNext()
        {
            Order nextOrder = null;

            // Serve Priority Queue first
            if (_priorityQueue.Count > 0)
            {
                nextOrder = _priorityQueue.Dequeue();
            }
            else if (_normalQueue.Count > 0)
            {
                nextOrder = _normalQueue.Dequeue();
            }

            if (nextOrder != null)
            {
                nextOrder.Status = OrderStatus.Dispatched;
            }

            return nextOrder;
        }

        public int TotalCount => _priorityQueue.Count + _normalQueue.Count;
    }

    // ==========================================
    // 4. DISPATCH ENGINE SYSTEM
    // ==========================================

    public class DispatchEngine
    {
        private readonly DispatchQueue _dispatchQueue = new DispatchQueue();
        private readonly LinkedList<DeliveryAgent> _agentRoster = new LinkedList<DeliveryAgent>();
        private readonly Stack<DispatchRecord> _dispatchHistory = new Stack<DispatchRecord>();

        // Repositories
        public Repository<Customer> Customers { get; } = new Repository<Customer>();
        public Repository<Restaurant> Restaurants { get; } = new Repository<Restaurant>();
        public Repository<Order> Orders { get; } = new Repository<Order>();

        // Roster Management
        public void AddAgent(DeliveryAgent agent)
        {
            _agentRoster.AddLast(agent);
        }

        public void QueueOrder(Order order)
        {
            Orders.Add(order);
            _dispatchQueue.Enqueue(order);
        }

        /// <summary>
        /// Dequeues the next agent from the front, assigns the order, and moves agent to back (rotating roster).
        /// O(1) time complexity using LinkedList operations.
        /// </summary>
        public DeliveryAgent GetNextAvailableAgent()
        {
            if (_agentRoster.Count == 0) return null;

            LinkedListNode<DeliveryAgent> firstNode = _agentRoster.First;
            _agentRoster.RemoveFirst();
            _agentRoster.AddLast(firstNode);

            return firstNode.Value;
        }

        /// <summary>
        /// Dispatches the next queued order to the next available agent.
        /// </summary>
        public DispatchRecord DispatchNextOrder()
        {
            if (_dispatchQueue.TotalCount == 0 || _agentRoster.Count == 0)
                return null;

            DeliveryAgent agent = GetNextAvailableAgent();
            Order order = _dispatchQueue.DispatchNext();

            var record = new DispatchRecord(order, agent);
            _dispatchHistory.Push(record);

            return record;
        }

        /// <summary>
        /// Pops last dispatch, sets order back to Queued, and moves agent back to the front of roster.
        /// </summary>
        public bool UndoLastDispatch()
        {
            if (_dispatchHistory.Count == 0) return false;

            DispatchRecord lastDispatch = _dispatchHistory.Pop();

            // 1. Revert order state
            lastDispatch.Order.Status = OrderStatus.Queued;

            // 2. Return agent to front of the roster
            // Locate current position in list and move to First
            LinkedListNode<DeliveryAgent> agentNode = _agentRoster.Find(lastDispatch.Agent);
            if (agentNode != null)
            {
                _agentRoster.Remove(agentNode);
                _agentRoster.AddFirst(agentNode);
            }

            // 3. Re-queue order into dispatch engine
            _dispatchQueue.Enqueue(lastDispatch.Order);

            return true;
        }

        // ==========================================
        // 5. REAL-TIME REPORTING FUNCTIONS
        // ==========================================

        /// <summary>
        /// HashSet ensures uniqueness of Customer IDs placed today.
        /// </summary>
        public HashSet<int> TodaysUniqueCustomerIds()
        {
            var uniqueCustomers = new HashSet<int>();
            DateTime today = DateTime.Today;

            foreach (var order in Orders)
            {
                if (order.PlacedAt.Date == today && order.Customer != null)
                {
                    uniqueCustomers.Add(order.Customer.Id);
                }
            }

            return uniqueCustomers;
        }

        /// <summary>
        /// Key-Value mapping of Restaurant ID to Menu Count below specified threshold.
        /// </summary>
        public Dictionary<int, int> LowAvailabilityRestaurants(int minMenuItems)
        {
            var result = new Dictionary<int, int>();

            foreach (var restaurant in Restaurants)
            {
                int count = restaurant.Menu?.Count ?? 0;
                if (count < minMenuItems)
                {
                    result[restaurant.Id] = count;
                }
            }

            return result;
        }

        /// <summary>
        /// Uses Dictionary for O(1) accumulation, then sorts into a List for final top N ranking.
        /// </summary>
        public List<(string ItemName, int TotalOrdered)> TopOrderedItems(int topN)
        {
            var counts = new Dictionary<string, int>();

            foreach (var order in Orders)
            {
                foreach (var item in order.Items)
                {
                    string name = item.MenuItem.Name;
                    if (!counts.ContainsKey(name))
                        counts[name] = 0;

                    counts[name] += item.Quantity;
                }
            }

            return counts.OrderByDescending(kvp => kvp.Value)
                         .Take(topN)
                         .Select(kvp => (ItemName: kvp.Key, TotalOrdered: kvp.Value))
                         .ToList();
        }

        /// <summary>
        /// Uses HashSet set operations (IsSupersetOf) to verify if customer ordered from both restaurants.
        /// </summary>
        public bool CustomerOrderedFromBothRestaurants(int customerId, int restaurantIdA, int restaurantIdB)
        {
            var visitedRestaurants = new HashSet<int>();

            foreach (var order in Orders)
            {
                if (order.Customer != null && order.Customer.Id == customerId && order.Restaurant != null)
                {
                    visitedRestaurants.Add(order.Restaurant.Id);
                }
            }

            var requiredRestaurants = new HashSet<int> { restaurantIdA, restaurantIdB };
            return visitedRestaurants.IsSupersetOf(requiredRestaurants);
        }
    }
}