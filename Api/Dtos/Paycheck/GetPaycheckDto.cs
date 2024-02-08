/// <summary>
/// Object returned from paycheck API endpoint
/// </summary>
public class GetPaycheckDto
{
    public int EmployeeId { get; set; }

    /// <summary>
    /// Calculated value of single paycheck after all deductions for the given employee
    /// </summary>
    public string PaycheckAmount { get; set; }
    public decimal PaycheckAmountNumeric { get; set; }
}