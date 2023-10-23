using System;
using System.Collections.Generic;

namespace TestMVC.Models;

public partial class PersonSkill
{
    public int IdList { get; set; }

    public int IdSkill { get; set; }

    public int IdPerson { get; set; }
    public byte Level { get; set; } = 1;

    public virtual Person IdPersonNavigation { get; set; } = null!;

    public virtual Skill IdSkillNavigation { get; set; } = null!;
}
