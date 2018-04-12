// Dont Cry For Me
// April 5 2018

// Characterizes how likely certain objects are to catch on fire
class Flammable {
  float multiplier;
  bool explodes;
  int burnTime;
  float damageMultiplier = 1.0;
  void Flammable(float multiplier, bool explodes, int burnTime = 0) {
    this.multiplier = multiplier;
    this.explodes = explodes;
    this.burnTime = burnTime;
  }
  void burn() {
    if (explodes) {
      explode();
    }
    else {
      // code here
    }
  }
  void explode() {
    //code here
  }
}

// Characterizes how easily objects can be destroyed
class Destructible {
  float durability;
  float minImpulse;
  void Destructible(float durability, float minImpulse) {
    this.durablility = durability;
    this.minImpulse = minImpulse;
  }
  void hit(float damage) {
    if (damage >= minImpulse) {
      // deal damage
      durability -= damage;
    }
  }
}

// Fluids overlay character, slow movement. (Ex: Water, Oil)
class Fluid {
  float viscosity;
  void Fluid(float viscosity) {
    this.viscosity = viscosity;
  }
}
