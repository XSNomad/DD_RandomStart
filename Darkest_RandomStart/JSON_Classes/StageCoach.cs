using System.Collections.Generic;

namespace Darkest_RandomStart.SC
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Requirements
    {
        [JsonProperty("number_of_quests_finished")]
        public int NumberOfQuestsFinished { get; set; }

        [JsonProperty("highest_dungeon_level")]
        public int HighestDungeonLevel { get; set; }
    }

    public class Upgrade
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("upgrade_tree_id")]
        public string UpgradeTreeId { get; set; }

        [JsonProperty("upgrade_requirement_code")]
        public string UpgradeRequirementCode { get; set; }
    }

    public class UpgradedRecruitUpgrade
    {
        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("chance")]
        public double Chance { get; set; }

        [JsonProperty("number_of_extra_positive_quirks")]
        public int NumberOfExtraPositiveQuirks { get; set; }

        [JsonProperty("number_of_extra_negative_quirks")]
        public int NumberOfExtraNegativeQuirks { get; set; }

        [JsonProperty("number_of_extra_combat_skills")]
        public int NumberOfExtraCombatSkills { get; set; }

        [JsonProperty("number_of_extra_camping_skills")]
        public int NumberOfExtraCampingSkills { get; set; }

        [JsonProperty("guaranteed_previous_raid_dead_hero_levels")]
        public List<int> GuaranteedPreviousRaidDeadHeroLevels { get; set; }

        [JsonProperty("upgrade_tree_id")]
        public string UpgradeTreeId { get; set; }

        [JsonProperty("upgrade_requirement_code")]
        public string UpgradeRequirementCode { get; set; }
    }

    public class HeroRecruitData
    {
        [JsonProperty("first_hero_classes")]
        public List<string> FirstHeroClasses { get; set; }

        [JsonProperty("number_of_recruits_upgrades")]
        public List<Upgrade> NumberOfRecruitsUpgrades { get; set; }

        [JsonProperty("roster_size_upgrades")]
        public List<Upgrade> RosterSizeUpgrades { get; set; }

        [JsonProperty("upgraded_recruits_upgrades")]
        public List<UpgradedRecruitUpgrade> UpgradedRecruitsUpgrades { get; set; }
    }

    public class Store
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("data")]
        public HeroRecruitData Data { get; set; }
    }

    public class RootObject
    {
        [JsonProperty("on_start_town_visit_priority")]
        public int OnStartTownVisitPriority { get; set; }

        [JsonProperty("requirements")]
        public Requirements Requirements { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("stores")]
        public List<Store> Stores { get; set; }
    }

}