using System;
using System.Collections.Generic;

namespace Assignment.DBO.Tables;

public partial class Account
{
    public int AccountId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string AccountHolderName { get; set; } = null!;

    public decimal Balance { get; set; }
}
