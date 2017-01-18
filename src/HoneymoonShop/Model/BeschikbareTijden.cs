using System;
using System.ComponentModel.DataAnnotations;

public class BeschikbareTijden
{
    [Key]
    public int ID { get; set; }

    public TimeSpan tijd { get; set; }
}




