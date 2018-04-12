// Dont Cry For Me
// April 5 2018

using attributes.cs;

class Fire {
  void Fire() {

  }
}

class Crops {
  void Crops() {

  }
}

class Wood {
  Flammable flammable;
  void Wood() {
    flammable = new Flammable(3, false, 10);
  }

}

class Water {
  void Water() {

  }
}
