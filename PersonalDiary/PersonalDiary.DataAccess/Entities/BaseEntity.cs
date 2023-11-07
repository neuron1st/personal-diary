﻿using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.DataAccess.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public Guid ExternalId { get; set; }
    public DateTime ModificationTime { get; set; }
    public DateTime CreationTIme { get; set; }

}

