using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetEntity
{
    public int Id;
    public string Name;
    public Sprite Image;
    public int Damage;
    public float AttackSpeed;
    public int AttackRange;
    public int Level;
    public int Price;

    public PetEntity(int Id, string Name, Sprite Image, int Damage, float AttackSpeed, int AttackRange, int Level, int Price)
    {
        this.Id = Id;
        this.Name = Name;
        this.Image = Image;
        this.Damage = Damage;
        this.AttackSpeed = AttackSpeed;
        this.AttackRange = AttackRange;
        this.Level = Level;
        this.Price = Price;
    }
}
