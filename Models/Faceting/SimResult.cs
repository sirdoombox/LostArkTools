namespace LostArkTools.Models.Faceting;

public struct SimResult
{
    public byte[] Counts { get; set; }
    public double Probability { get; set; }
    public double Score { get; set; }
}