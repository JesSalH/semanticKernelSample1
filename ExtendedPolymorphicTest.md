# Polymorphic Extended Character API Test

## Updated Architecture: Extended Character Types

The ApiAlphaPlugin now supports full polymorphic extended character types with class-specific properties:

### Extended Character Models:

#### 1. ExtendedFighterClassAttributes
```csharp
// Base properties: Weapon, AlternateWeapon, ArmorType, Skills
// Fighter-specific properties:
- WeaponMastery: int
- ArmorRating: int  
- BattleEndurance: int
- BattleCry: string
- SpecialMeleeAttack: string
```

#### 2. ExtendedRogueClassAttributes
```csharp
// Base properties: Weapon, AlternateWeapon, ArmorType, Skills
// Rogue-specific properties:
- Stealth: int
- Agility: int
- LockPicking: int
- RogueSchool: string
- MasterSkill: string
```

#### 3. ExtendedMageClassAttributes
```csharp
// Base properties: Weapon, AlternateWeapon, ArmorType, Skills
// Mage-specific properties:
- MagicType: string
- Spellcasting: int
- ManaPool: int
- Memory: int
- ArcaneResistance: int
- Spells: List<Spell>
- Companion: string
```

### JSON Deserialization Flow:

1. **API Response**: Contains `classType` field ("fighter", "rogue", "mage")
2. **ExtendedClassAttributesConverter**: Routes to appropriate extended class
3. **Type Safety**: Each character type has proper typed properties
4. **Inheritance**: All extended classes inherit base properties (Weapon, AlternateWeapon, ArmorType, Skills)

### Example API Responses Supported:

#### Fighter (Hero):
```json
{
  "name": "Hero",
  "strength": 10,
  "resilience": 8,
  "wounds": 7,
  "classAttributes": {
    "classType": "fighter",
    "weaponMastery": 5,
    "armorRating": 7,
    "battleEndurance": 6,
    "battleCry": "For honor!",
    "specialMeleeAttack": "Berserker Barrage",
    "weapon": { "name": "Battle Axe", "description": "Two-handed battle axe" },
    "alternateWeapon": { "name": "Short Sword and Buckler", "description": "Short sword and small shield" },
    "armorType": "Heavy Armor",
    "skills": ["Block", "Counter Attack", "Throw Back", "Stun Strike", "Throw Down", "Power Strike"]
  }
}
```

#### Rogue (Shadow):
```json
{
  "name": "Shadow",
  "strength": 6,
  "resilience": 6,
  "wounds": 3,
  "classAttributes": {
    "classType": "rogue",
    "stealth": 9,
    "agility": 8,
    "lockPicking": 7,
    "rogueSchool": "Shadows Master",
    "masterSkill": "Shadow Step",
    "weapon": { "name": "Sword and Dagger", "description": "Sword and dagger" },
    "alternateWeapon": { "name": "Throwable knifes", "description": "Throwable knifes" },
    "armorType": "Light Armor",
    "skills": ["Dodge", "Stealth", "Backstab", "Vanish"]
  }
}
```

#### Mage (Merlin Jr):
```json
{
  "name": "Merlin Jr",
  "strength": 4,
  "resilience": 5,
  "wounds": 2,
  "classAttributes": {
    "classType": "mage",
    "magicType": "Fire Magic",
    "spellcasting": 8,
    "manaPool": 100,
    "memory": 12,
    "arcaneResistance": 9,
    "spells": [
      {
        "name": "Explosive Fireball",
        "description": "Launches a big ball of fire at the enemy that explodes at impact, dealing a huge amount damage.",
        "magicType": "Fire Magic",
        "memory": 5,
        "manaCost": 9
      }
    ],
    "companion": "Salamander",
    "weapon": { "name": "The Inferno Staff", "description": "A staff that channels the fury of fire" },
    "alternateWeapon": { "name": "Dagger", "description": "A small dagger for close combat" },
    "armorType": "Robe",
    "skills": ["Cast Spell", "Mana Shield", "Teleport", "Summon Familiar"]
  }
}
```

## Test Commands:

When running the application, test with:

1. **"Get extended character details for ID 1"** - Should return fully typed Fighter with all fighter-specific properties
2. **"Show me the extended information for character 2"** - Should return Rogue with stealth and agility stats
3. **"Get the detailed character info for ID 3"** - Should return Mage with spells, companion, and mana pool

The JSON deserializer will automatically create the correct extended class type based on the `classType` field, providing full type safety and intellisense support!
