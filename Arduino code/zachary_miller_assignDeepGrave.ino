#include <ezButton.h>


// https://www.iottechtrends.com/integrate-arduino-with-unity-3d/ use this for arduino to unity
#define LEDW_1 2
#define LEDW_2 3
#define LEDW_3 4
#define LEDW_4 5
#define LEDR 10
#define LEDY 11
#define LEDB 12
#define LEDG 13

#define BUTTON_R 8
#define BUTTON_L 7

#define SENSOR_PIN A0

const int MAX_DELAY = 2000;

ezButton buttonR(BUTTON_R);
ezButton buttonL(BUTTON_L);

void setup() {
  pinMode(LEDW_1, OUTPUT);
  pinMode(LEDW_2, OUTPUT);
  pinMode(LEDW_3, OUTPUT);
  pinMode(LEDW_4, OUTPUT);
  pinMode(LEDR, OUTPUT);
  pinMode(LEDG, OUTPUT);
  pinMode(LEDB, OUTPUT);
  pinMode(LEDY, OUTPUT);

  pinMode(SENSOR_PIN, INPUT);

  Serial.begin(9600);

  testFunction();
  setAllLedsOn();
}

void loop() {
  buttonR.loop();
  buttonL.loop();

  interactSerial();

  delay(1);
}

void interactSerial() {
  //takes all of the ints in the Serial
  unsigned long serial = Serial.parseInt();
  //takes only the last one and uses it tell the arduino to do things.
//  serial = serial % 10;
//  Serial.print(serial);
//  Serial.print(",");
  if (serial % 10000 / 1000 == 1) {
    digitalWrite(LEDG, HIGH);
  }else{
    digitalWrite(LEDG, LOW);
  }

  if (serial % 1000 / 100 == 1) {
    digitalWrite(LEDB, HIGH);
  }else{
    digitalWrite(LEDB, LOW);
  }

  if (serial % 100 / 10 == 1) {
    digitalWrite(LEDY, HIGH);
  }else{
    digitalWrite(LEDY, LOW);
  }

  if (serial % 10 == 1) {
    digitalWrite(LEDR, HIGH);
  }else{
    digitalWrite(LEDR, LOW);
  }

  switch (serial / 10000) {
    case 0:
    digitalWrite(LEDW_1, LOW);
    case 1:
    digitalWrite(LEDW_2, LOW);
    case 2:
    digitalWrite(LEDW_3, LOW);
    case 3:
    digitalWrite(LEDW_4, LOW);
      break;
    case 4:
    setAllLedsOn();
      break;
  }

  printAllToSerial();

  Serial.flush();
}

void setAllLedsOn() {
  digitalWrite(LEDW_1, HIGH);
  digitalWrite(LEDW_2, HIGH);
  digitalWrite(LEDW_3, HIGH);
  digitalWrite(LEDW_4, HIGH);
}

void setAllLedsOff() {
  digitalWrite(LEDW_1, LOW);
  digitalWrite(LEDW_2, LOW);
  digitalWrite(LEDW_3, LOW);
  digitalWrite(LEDW_4, LOW);
}

//prints all of the game needed variables to the serial
void printAllToSerial() {
  Serial.print(analogRead(SENSOR_PIN));
  Serial.print(",");
  Serial.print(buttonR.getState());
  Serial.print(",");
  Serial.println(buttonL.getState());
}

//Tests all the lights and servos
void testFunction() {
  digitalWrite(LEDW_1, HIGH);
  digitalWrite(LEDW_2, HIGH);
  digitalWrite(LEDW_3, HIGH);
  digitalWrite(LEDW_4, HIGH);
  digitalWrite(LEDR, HIGH);
  digitalWrite(LEDB, HIGH);
  digitalWrite(LEDY, HIGH);
  digitalWrite(LEDG, HIGH);
  delay(MAX_DELAY);
  digitalWrite(LEDW_1, LOW);
  digitalWrite(LEDW_2, LOW);
  digitalWrite(LEDW_3, LOW);
  digitalWrite(LEDW_4, LOW);
  digitalWrite(LEDR, LOW);
  digitalWrite(LEDB, LOW);
  digitalWrite(LEDY, LOW);
  digitalWrite(LEDG, LOW);
}
