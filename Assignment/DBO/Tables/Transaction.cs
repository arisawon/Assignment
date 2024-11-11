using System;
using System.Collections.Generic;

namespace Assignment.DBO.Tables;

/// <summary>
/// Class corrosponding to the Transaction table of the database.
/// </summary>
public partial class Transaction
{
    public long TransactionId { get; set; }

    public DateTime TransactionDate { get; set; }

    public string FromAccount { get; set; } = null!;

    public string ToAccount { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? TransactionDescription { get; set; }
}
