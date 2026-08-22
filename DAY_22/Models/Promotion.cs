public abstract class Promotion
{
    public string Code { get; set; }
}

public class PercentOffPromotion : Promotion
{
    public double PercentOff { get; set; }
}

public class FlatAmountPromotion : Promotion
{
    public decimal AmountOff { get; set; }
}

public class BuyOneGetOnePromotion : Promotion
{
}
