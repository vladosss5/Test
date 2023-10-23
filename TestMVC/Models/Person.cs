using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVC.Models;

public partial class Person
{
    [Key]
    public int IdPerson { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;
    
    public virtual ICollection<PersonSkill> PersonSkills { get; set; } = new List<PersonSkill>();
}
