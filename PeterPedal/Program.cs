using System;
using System.Collections.Generic;

// Customer contact details for a repair case.
class RepairCaseData
{
    public string FirstName;
    public string LastName;
    public string Phone;
}

class RepairCase
{
    public string FrameNumber;
    public string Problem;
    public RepairCaseData CustomerInfo;
    public List<string> Findings = new List<string>();
    public List<string> Parts = new List<string>();
    public int Status; // 0 = created, 1 = awaiting approval, 2 = approved, 3 = finished
    public Boolean Approved;
    public decimal TotalPrice;
}

class SparePartCatalog
{
    private Dictionary<string, decimal> prices = new Dictionary<string, decimal>
    {
        { "Gear cable", 150m },
        { "Sprocket", 300m },
        { "Brake pads", 120m }
    };

    public decimal GetPrice(string partName)
    {
        if (prices.ContainsKey(partName))
        {
            return prices[partName];
        }
        return 0m;
    }
}

class Notifier
{
    public void SendSms(string phone, String message)
    {
        Console.WriteLine("SMS to " + phone + ": " + message);
    }

    public void LeaveVoicemail(string phone)
    {
        Console.WriteLine($"Voicemail left for {phone}: please call us back regarding your bike.");
    }
}

class repairService
{
    private List<RepairCase> cases = new List<RepairCase>();
    private SparePartCatalog catalog = new SparePartCatalog();
    private Notifier notifier = new Notifier();

    private const decimal HOURLY_RATE = 450;

    public void CreateCase(string firstName, string lastName, string phone, string FrameNumber, string problem)
    {
        RepairCaseData customer = new RepairCaseData();
        customer.FirstName = firstName;
        customer.LastName = lastName;
        customer.Phone = phone;

        RepairCase c = new RepairCase();
        c.FrameNumber = FrameNumber;
        c.Problem = problem;
        c.CustomerInfo = customer;
        c.Status = 0;

        cases.Add(c);

        Console.WriteLine($"Case created for {customer.FirstName} {customer.LastName}, frame number {FrameNumber}.");
        Console.WriteLine($"Problem: {problem}");
    }

    public void registerFindings(string frameNumber, List<string> findings)
    {
        RepairCase c = FindCase(frameNumber);
        if (c != null)
        {
            if (findings != null)
            {
                if (findings.Count > 0)
                {
                    foreach (var finding in findings)
                    {
                        if (finding != "")
                        {
                            c.Findings.Add(finding);
                            Console.WriteLine("Finding registered: " + finding);
                        }
                    }
                }
            }
        }
    }

    public void LookUpParts(string frameNumber)
    {
        RepairCase c = FindCase(frameNumber);

        foreach (var finding in c.Findings)
        {
            string stlnr = c.FrameNumber;
            if (finding.Contains("Gear cable"))
            {
                c.Parts.Add("Gear cable");
                Console.WriteLine($"Found part for case {stlnr}: Gear cable ({catalog.GetPrice("Gear cable")} kr)");
            }
            else if (finding.Contains("Sprocket"))
            {
                c.Parts.Add("Sprocket");
                Console.WriteLine($"Found part for case {stlnr}: Sprocket ({catalog.GetPrice("Sprocket")} kr)");
            }
            else if (finding.Contains("Brake pads"))
            {
                c.Parts.Add("Brake pads");
                Console.WriteLine($"Found part for case {stlnr}: Brake pads ({catalog.GetPrice("Brake pads")} kr)");
            }
        }

        Int32 numberOfParts = c.Parts.Count;
        Console.WriteLine($"Found {numberOfParts} part(s) for case {frameNumber}.");
    }

    // Calculates the price of a gear cable including markup.
    private decimal CalculatePriceForGearCable()
    {
        decimal price = 150m;
        decimal markup = price * 0.1m;
        return price + markup;
    }

    // Calculates the price of a sprocket including markup.
    private decimal CalculatePriceForSprocket()
    {
        decimal price = 300m;
        decimal markup = price * 0.1m;
        return price + markup;
    }

    // Calculates the price of brake pads including markup.
    private decimal CalculatePriceForBrakePad()
    {
        decimal price = 120m;
        decimal markup = price * 0.1m;
        return price + markup;
    }

    // Calculates a price estimate for the customer's offer.
    private decimal BeregnPris(RepairCase c)
    {
        decimal partsPrice = CalculatePriceForGearCable() + CalculatePriceForSprocket() + CalculatePriceForBrakePad();
        decimal labor = HOURLY_RATE * 2;
        decimal subtotal = partsPrice + labor;
        decimal vat = subtotal * 0.25m;
        return subtotal + vat;
    }

    public void CalculateOffer(string frameNumber)
    {
        RepairCase c = FindCase(frameNumber);
        decimal Price = BeregnPris(c);
        c.TotalPrice = Price;
        c.Status = 1;

        int d = 3;
        string cstTlf = c.CustomerInfo.Phone;
        Console.WriteLine($"Offer for case {frameNumber}: {Price.ToString("F2")} kr, delivery in {d} days.");
        Console.WriteLine($"Calling {cstTlf}...");
        notifier.LeaveVoicemail(cstTlf);
    }

    public void ApproveCase(string frameNumber)
    {
        RepairCase c = FindCase(frameNumber);
        c.Approved = true;
        c.Status = 2;
        Console.WriteLine($"{c.CustomerInfo.FirstName} accepted the offer.");
    }

    // Sofia repairs the bike.
    public void PimpMyBike(string frameNumber) {
        RepairCase c = FindCase(frameNumber);
        Console.WriteLine($"Sofia is repairing the bike, frame number {c.FrameNumber}...");
    }

    // Calculates the final total price for the receipt.
    private decimal CalculateTotal(RepairCase c)
    {
        decimal partsPrice = CalculatePriceForGearCable() + CalculatePriceForSprocket() + CalculatePriceForBrakePad();
        decimal labor = HOURLY_RATE * 2;
        decimal subtotal = partsPrice + labor;
        decimal vat = subtotal * 0.25m;
        return subtotal + vat;
    }

    public void finishRepair(string frameNumber)
    {
        RepairCase c = FindCase(frameNumber);

        if (c.Status == 2 && c.Parts.Count > 0 && c.Approved)
        {
            decimal total = CalculateTotal(c);

            if (total < 0)
            {
                Console.WriteLine("Error: negative price");
            }

            c.TotalPrice = total;
            c.Status = 3;

            String message = "Hi " + c.CustomerInfo.FirstName + ", your bike is ready for pickup!";
            notifier.SendSms(c.CustomerInfo.Phone, message);

            Console.WriteLine("--- Receipt ---");
            Console.WriteLine("Frame number: " + c.FrameNumber);
            Console.WriteLine("Total: " + Math.Round(total, 2) + " kr");
        }
    }

    public void PayCase(string frameNumber)
    {
        var result = FindCase(frameNumber);
        Console.WriteLine($"{result.CustomerInfo.FirstName} has paid {result.TotalPrice.ToString("F2")} kr. The bike is ready to ride!");
    }

    // Old summary print, replaced by the receipt in finishRepair(). No longer called anywhere.
    public void PrintCaseSummary(string frameNumber) {
	RepairCase c = FindCase(frameNumber);
	Console.WriteLine("Case summary for " + c.FrameNumber + ": " + c.Problem);
    }

    private RepairCase FindCase(string frameNumber)
    {
        foreach (var c in cases)
        {
            if (c.FrameNumber == frameNumber)
            {
                return c;
            }
        }
        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var service = new repairService();

        service.CreateCase("Egon", "Hansen", "20123456", "STL-4471", "The gears are not shifting properly and the bike is almost impossible to ride.");
        service.registerFindings("STL-4471", new List<string> { "Gear cable needs replacement", "Sprocket is worn", "Brake pads are worn" });
        service.LookUpParts("STL-4471");
        service.CalculateOffer("STL-4471");
        service.ApproveCase("STL-4471");
        service.PimpMyBike("STL-4471");
        service.finishRepair("STL-4471");
        service.PayCase("STL-4471");
    }
}
