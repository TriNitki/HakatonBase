﻿namespace Base.Contracts.Merch;

public class CreateMerchRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public uint Stock { get; set; }
    public double Price { get; set; }
}
