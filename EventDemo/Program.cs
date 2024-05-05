// See https://aka.ms/new-console-template for more information


Stock stock = new Stock("THPW");
stock.Price = 27.10M;
stock.PriceChanged += stock_PriceChanged;
stock.Price = 35.59M;

void stock_PriceChanged(object? sender, PriceChangedEventArgs e)
{
    if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
        Console.WriteLine("Alert, 10% stock price increase!");
}


public class Stock
{
    string symbol;
    decimal price;
    public Stock (string symbol) => this.symbol = symbol;

    public event EventHandler<PriceChangedEventArgs> PriceChanged;

    public virtual void OnPriceChanged(PriceChangedEventArgs e)
    { 
        PriceChanged?.Invoke(this, e); 
    }

    public decimal Price
    {
        get => price;
        set
        {
            if (price == value) return;
            decimal oldPrice = price;
            price = value;
            OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
        }
    }
}

public class PriceChangedEventArgs : EventArgs
{ 
    public readonly decimal LastPrice;
    public readonly decimal NewPrice;

    public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
    { 
        LastPrice = lastPrice;
        NewPrice = newPrice;
    }
}



