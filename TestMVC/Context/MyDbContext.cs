﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestMVC.Models;

namespace TestMVC.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<PersonSkill> PersonSkills { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;user id=postgres;password=toor;database=Test;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.IdPerson).HasName("Persons_pk");

            entity.Property(e => e.IdPerson).UseIdentityAlwaysColumn();
            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PersonSkill>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("PersonSkill_pk");

            entity.ToTable("PersonSkill");

            entity.Property(e => e.IdList).UseIdentityAlwaysColumn();
            
            entity.Property(e => e.Level);

            entity.HasOne(d => d.IdPersonNavigation).WithMany(p => p.PersonSkills)
                .HasForeignKey(d => d.IdPerson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PersonSkill_Persons_IdPerson_fk");

            entity.HasOne(d => d.IdSkillNavigation).WithMany(p => p.PersonSkills)
                .HasForeignKey(d => d.IdSkill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PersonSkill_Skills_IdSkill_fk");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.IdSkill).HasName("Skills_pk");

            entity.Property(e => e.IdSkill).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
