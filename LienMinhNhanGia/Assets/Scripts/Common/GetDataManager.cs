using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetDataManager
{
    public static List<ItemEntity> ListShopItem = new ItemDAO().GetAllItem();

    public static List<SkillEntity> ListShopSkill =  new SkillDAO().GetAllSkill();
}
