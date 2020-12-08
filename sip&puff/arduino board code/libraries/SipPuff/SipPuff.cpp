#include "SipPuff.h"
const int numReadings = 5;     // Number of readings to buffer
int sensorValues[numReadings];
int sensorAverage;
long x;
float preSipAndPuffPower;

SipPuff::SipPuff(int pin)
{
	_state = 0;  			  // initial sensor state (off=0, sip=-1, puff=1)
	_lastState = _state;
	active = 0;			  //public variable to check if sensor is active  (sipped or puffed)
	_inputPattern = "";			  //temporary string to store input pattern
	finalPattern = "";			  //final pattern sequence
	_lastBounceTime = 0;
	debounceTime = 2;            // debounce time (ms)
	multiStateTime = 250;           // time limit for multiple sip/puff inputs
	longStateTime = 0;          // time until long sips/puffs register
	deadZone = 7;            //size of pressure sensor "dead zone"
	_pinNum = pin;
	_nomPressure = analogRead(_pinNum);		//variable to store nominal atmospheric pressure
	lastPressure = 0;			  //stores highest/lowest value of the the last input pattern for additional input criteria
	SipPower = 0.1;
	pressurePower = 0;
	x = 0;
	lastPressure = _nomPressure;
	c_done = false;
}








bool endsignal;

String endmessagge;
void SipPuff::CommandRecoginationTest(controllerConfigurations& i_controllerConfiguration, HardwareSerial& o_Serial, InputDeviceMessage& i_inputDeviceMessageManager) {
	if (cp_sp_signal) {

		finalPattern = "";
	}
	if (cp_sp_signal) {


		finalPattern = _inputPattern;
		endmessagge = _inputPattern;
	}
	if (cp_sp_signal && (long)(millis()) > x + 40) {

		x = (long)millis();
		if (SipPower <= .97f)
			SipPower += .03f;


	}

	if (endsignal) {

		//SipPower = 0.001f;
		finalPattern = endmessagge;
		endsignal = false;
	}
	for (int count = 0; count < i_controllerConfiguration.sizeOfcommands; count++) {
		if (finalPattern != "" && finalPattern == i_controllerConfiguration.Commands[count])
		{



			if (abs(SipPower - preSipAndPuffPower) > .011) {
				//Test Loop for sip and puff
				i_inputDeviceMessageManager.copyValues(i_controllerConfiguration.Encodes[count], SipPower);
				//SipPower = SipPuffSpeedVal;
				preSipAndPuffPower = SipPower;
			}
			if (_inputPattern == "") {
				finalPattern = "";
				//SipPower = .1f;
			}
		}
		if (finalPattern == "SSS") {

			i_inputDeviceMessageManager.copyValues("Restart", SipPower);

		}
	}

}



























void SipPuff::UpdateC() {
	long now = (long)millis();  //store current time
	readSensor();
	// Detect Sip/Puff above deadzone threshold and change sensor state accordignly
	if (sensorAverage < _nomPressure - deadZone) {

		_state = -1;

		//
		if (sensorAverage < lastPressure)
			lastPressure = analogRead(_pinNum);

	}
	else if (sensorAverage > _nomPressure + deadZone) {

		_state = 1;
		if (sensorAverage > lastPressure)
			lastPressure = sensorAverage;

	}
	else _state = 0;

	// If the state changed, due to noise or sip/puff, reset the debounce timer
	if (_state != _lastState) {
		_lastBounceTime = now;
	}

	//If a stable change has taken place, record the type of input and swap value of 'active'
	if (now - _lastBounceTime > debounceTime && abs(_state) != active) {

		active = abs(_state);

		if (active) {

			lastPressure = _nomPressure;

			if (_state > 0)
				_inputPattern += "P";

			if (_state < 0)
				_inputPattern += "S";

		}
	}

	//If a stable nominal state is present, record the final pattern into the finalPattern String
	if (now - _lastBounceTime > multiStateTime) {
		c_done = (!active && _inputPattern != "");
		finalPattern = _inputPattern;
		if(!active)
		_inputPattern = "";
	}
	_lastState = _state;
}

//Call this function to zero out the pressure sensor to atmospheric ambient pressure
bool SipPuff::setNominalPressure() {
	_nomPressure = analogRead(_pinNum);

	if (_nomPressure == 0)
		return false;

	lastPressure = _nomPressure;
	return true;
}

bool SipPuff::setNominalPressure(int val) {
	 
	_nomPressure = val;

	if (_nomPressure == 0)
		return false;
	lastPressure = _nomPressure;

	return true;
}


void SipPuff::readSensor() {
	// Take a few readings
	for (int i = 0; i < numReadings; i++) {
		int v = analogRead(_pinNum);
		sensorValues[i] = v;

		//delay(2);
	}

	// Find average of all readings
	int total = 0;
	for (int i = 0; i < numReadings; i++)
		total += sensorValues[i];

	sensorAverage = total / numReadings;
}
void SipPuff::update() {
	long now = (long)millis();  //store current time
	readSensor();
	// Detect Sip/Puff above deadzone threshold and change sensor state accordignly
	if (sensorAverage < _nomPressure - deadZone) {

		_state = -1;
	 
		//
		if (sensorAverage < lastPressure)
			lastPressure = analogRead(_pinNum);

	}
	else if (sensorAverage > _nomPressure + deadZone) {

		_state = 1;
 		if (sensorAverage > lastPressure)
			lastPressure = sensorAverage;

	}
	else _state = 0;

	// If the state changed, due to noise or sip/puff, reset the debounce timer
	if (_state != _lastState) {
		_lastBounceTime = now;
	}

	//If a stable change has taken place, record the type of input and swap value of 'active'
	if (now - _lastBounceTime > debounceTime && abs(_state) != active) {

		active = abs(_state);

		if (active) {

			lastPressure = _nomPressure;
			 
			if (_state > 0)
				_inputPattern += "P";

			if (_state < 0)
				_inputPattern += "S";
		}
	}

	//If a stable nominal state is present, record the final pattern into the finalPattern String
	if (((!active) && now - _lastBounceTime > multiStateTime) ) {
		finalPattern = _inputPattern;
		 
			_inputPattern = "";
	}
	_lastState = _state;

}






/* Test function for updating sip and puff using inputs through the function this is the same as the traditional update() function */
int lastPressureT;
void SipPuff::updateT(int val) {
	long now = (long)millis();  //store current time
	//readSensor();
	// Detect Sip/Puff above deadzone threshold and change sensor state accordignly
	if (val < _nomPressure - deadZone) {

		_state = -1;

		//
		if (sensorAverage < lastPressureT)
			lastPressureT = analogRead(_pinNum);

	}
	else if (val > _nomPressure + deadZone) {

		_state = 1;
		if (sensorAverage > lastPressureT)
			lastPressureT = sensorAverage;

	}
	else _state = 0;

	// If the state changed, due to noise or sip/puff, reset the debounce timer
	if (_state != _lastState) {
		_lastBounceTime = now;
	}

	//If a stable change has taken place, record the type of input and swap value of 'active'
	if (now - _lastBounceTime > debounceTime && abs(_state) != active) {

		active = abs(_state);

		if (active) {

			lastPressureT = _nomPressure;

			if (_state > 0)
				_inputPattern += "P";

			if (_state < 0)
				_inputPattern += "S";
		}
	}

	//If a stable nominal state is present, record the final pattern into the finalPattern String
	if (((!active) && now - _lastBounceTime > multiStateTime) ) {
		finalPattern = _inputPattern;

			_inputPattern = "";
	}
	_lastState = _state;

}














bool left;
bool right;
void SipPuff::update(int val, bool reset) {
	long now = (long)millis();  //store current time
	if (reset) {
		_state = _lastState = 0;
		finalPattern = "";
		_inputPattern = "";
		_lastBounceTime = 0;
	//_lastBounceTime = now;
		return;
	}
	
	//readSensor();
	// Detect Sip/Puff above deadzone threshold and change sensor state accordignly
	if (val < _nomPressure - deadZone) {

		_state = -1;
left =true;
		//
		 

	}
	else if (val > _nomPressure + deadZone) {

		_state = 1;
		 right =true;

	}
	else _state = 0;

	// If the state changed, due to noise or sip/puff, reset the debounce timer
	if (_state != _lastState) {
		_lastBounceTime = now;
	}

	//If a stable change has taken place, record the type of input and swap value of 'active'
	if (now - _lastBounceTime > debounceTime&& abs(_state) != active) {

		active = abs(_state);

		if (active) {

			lastPressure = _nomPressure;

			if (_state > 0)
				_inputPattern += "P";

			if (_state < 0)
				_inputPattern += "S";
		}
	}

	//If a stable nominal state is present, record the final pattern into the finalPattern String
	if (((!active) && now - _lastBounceTime > multiStateTime) || _inputPattern.length() == 2) {
		finalPattern = _inputPattern;

		_inputPattern = "";
	 left=false;
		 right= false;
		_lastBounceTime = 0;
		_state = _lastState = 0;
	}
	  
	if(now- _lastBounceTime > multiStateTime  ){
		 if(left)
			 finalPattern="S";
		 else if (right)
			 finalPattern="P";
		 
		 left=false;
		 right= false;
 _lastBounceTime = 0;
		_state = _lastState = 0;
	}
	_lastState = _state;

}




void SipPuff::UpdateBeta() {





}



	void  SipPuff::updateTest() {
		long now = (long)millis();  //store current time
		readSensor();
		// Detect Sip/Puff above deadzone threshold and change sensor state accordignly
		if ( sensorAverage < lastPressure - deadZone  ) {

			_state = -1;

			// 

			if(sensorAverage< maxPressure)
			maxPressure = sensorAverage;

		}
		else if ( sensorAverage > lastPressure + deadZone ) {

			_state = 1;

			if (sensorAverage > maxPressure)
			maxPressure = sensorAverage;
		}



		else {


			_state = 0;

			}


			 

		 

			 

	 

		// If the state changed, due to noise or sip/puff, reset the debounce timer
		if (_state != _lastState) {
			_lastBounceTime = now;
		}

		//If a stable change has taken place, record the type of input and swap value of 'active'
		if (now - _lastBounceTime > debounceTime && abs(_state) != active) {

			active = abs(_state);


			if (active) {

				//lastPressure = _nomPressure;

				if (_state > 0) {

					Serial.println("P");
					_inputPattern += "P";
					
				}



				if (_state < 0) {
					_inputPattern += "S";
				


				}


			}
		}

		//If a stable nominal state is present, record the final pattern into the finalPattern String
		if (!active && now - _lastBounceTime > multiStateTime) {
			finalPattern = _inputPattern;
			
		//_lastBounceTime = now;
		 
				_inputPattern = "";
		}
		_lastState = _state;

}


//void SipPuff::CommandRecogination(controllerConfigurations  &   i_controllerConfiguration, HardwareSerial & o_Serial){
//
//
//
//
//
//}
//void SipPuff::CommandRecoginationTetraSki(controllerConfigurations& i_controllerConfiguration, InputDeviceMessage& i_inputDeviceMessageManager) {
//
//	for (int count = 0; count < i_controllerConfiguration.sizeOfcommands; count++) {
//		if (finalPattern != "" && finalPattern == i_controllerConfiguration.Commands[count])
//		{
//
//			if (finalPattern == "S" || finalPattern == "P")
//				SipPower += .01;
//			i_inputDeviceMessageManager.copyValues(i_controllerConfiguration.Encodes[count], SipPower);
//			SipPower = SipPuffSpeedVal;
//		}
//
//	}
//
//}


void SipPuff::CommandRecogination(controllerConfigurations & i_controllerConfiguration,InputDeviceMessage & i_inputDeviceMessageManager) {
	SipPower = 1;
	for (int count = 0; count < i_controllerConfiguration.sizeOfcommands; count++) {
		if (finalPattern != "" && finalPattern == i_controllerConfiguration.Commands[count])
		{ 
				 				
			//Test Loop for sip and puff
			i_inputDeviceMessageManager.copyValues(i_controllerConfiguration.Encodes[count], SipPower);
			if (finalPattern == "SS" || finalPattern == "PP")
				_inputPattern = "";
			//SipPower = SipPuffSpeedVal;
		}
	 
	}

}

 
bool writeagain;
void ConfirmResponse(HardwareSerial & o_Serial, const char* data) {
	o_Serial.flush();
	delay(300);

	while (o_Serial.available() > 0) {


		o_Serial.print(data);
		o_Serial.readString();
		o_Serial.flush();
		o_Serial.flush();

	}





}






void SipPuff::calibrate(HardwareSerial & o_Serial) {
	int puffTime = 0, shortSipAvg = 0, calCount = 0;
	_lastState = 1;



	//
	//while (!o_Serial.writeBLEUart("c1\n")) {

	   // delay(100);
	   // o_Serial.writeBLEUart("c1\n");

	   //}

	o_Serial.print("c1\n");
	ConfirmResponse(o_Serial, "c1\n");
	// o_Serial.writeBLEUart("cool");


 //o_Serial.print("c5");
 //o_Serial.flush();




















  //SHORT SIP - useful to compare with a long sip to determine the difference
	while (calCount < 3) {

		if (analogRead(_pinNum) < _nomPressure - deadZone)
			_state = 0;    //Read in value of sip switch
		else
			_state = 1;

		//Detect sip, start debounce timer
		if (_state == LOW && _lastState == HIGH)
			_lastBounceTime = millis();

		//Detect sip End, check against debounce timer, increment if successful reading
		if (_state == HIGH && _lastState == LOW && (millis() - _lastBounceTime > debounceTime)) {
			puffTime = millis() - _lastBounceTime;

			o_Serial.print("+1\n");
			ConfirmResponse(o_Serial, "+1\n");
			shortSipAvg += puffTime;
			calCount++;
		}

		_lastState = _state;    //Record old value of switch to compare next iteration
	}
	int shortAvrage = shortSipAvg / 3;

	//o_Serial.println( shortSipAvg/3);




	o_Serial.print("c2\n");
	ConfirmResponse(o_Serial, "c2\n");

	_lastState = 1;
	calCount = 0;


	_state = 0;
	_lastState = 0;

	_lastBounceTime = 0;

	//SHORT SIP - useful to compare with a long sip to determine the difference
	while (calCount < 3) {

		if (analogRead(_pinNum) > _nomPressure + deadZone) {
			_state = 1;    //Read in value of sip switch

		}
		else
			_state = 0;

		//Detect puff, start debounce timer
		if (_state == 1 && _lastState == 0)
			_lastBounceTime = millis();

		//Detect puff End, check against debounce timer, increment if successful reading
		if (_state == 0 && _lastState == 1 && (millis() - _lastBounceTime > debounceTime)) {
			puffTime = millis() - _lastBounceTime;

			o_Serial.print("11\n");

			ConfirmResponse(o_Serial, "11\n");

			shortSipAvg += puffTime;
			calCount++;
		}

		_lastState = _state;    //Record old value of switch to compare next iteration
	}



	if (shortSipAvg / 3 > shortAvrage)
		shortAvrage = shortSipAvg / 3;


	o_Serial.print("c3\n");
	ConfirmResponse(o_Serial, "c3\n");

	calCount = 0;
	_state = 0;
	_lastState = 1;

	int longSipAvg = 0;
	//LONG SIP - Tests the average length of a long sip to determine the appropriate long sip threshold
	while (calCount < 3) {

		if (analogRead(_pinNum) < _nomPressure - deadZone)
			_state = 0;    //Read in value of sip switch
		else
			_state = 1;

		//Detect sip, start debounce timer
		if (_state == LOW && _lastState == HIGH)
			_lastBounceTime = millis();

		//Detect sip end, check against debounce timer, increment if successful reading
		if (_state == HIGH && _lastState == LOW && (millis() - _lastBounceTime > debounceTime) && (millis() - _lastBounceTime) > shortAvrage) {
			puffTime = millis() - _lastBounceTime;  //Calculate length of puff
			longSipAvg += puffTime;
			o_Serial.print("+2\n");
			ConfirmResponse(o_Serial, "+2\n");

			calCount++;   //Increment counter
		}

		_lastState = _state;   //Record old value of switch to compare next iteration
	}

	int LongAverage = longSipAvg / 3;

	if (LongAverage - shortAvrage > 200) {


		longStateTime = shortAvrage + 200;
	}

	else {
		longStateTime = LongAverage;


	}
	//o_Serial.println(longSipAvg/3);

   //Reset counter and switch value for next test
	_lastState = 1;
	calCount = 0;


	puffTime = 0;

	bool count = false;
	int  doubleSipAvg = 0;



	delay(1000);
	_state = _lastState = 0;
	o_Serial.print("c4\n");
	ConfirmResponse(o_Serial, "c4\n");


	calCount = 0;
	//DOUBLE SIP - Tests the average length of a double sip to determine the appropriate double sip threshold
	while (calCount < 3) {
		while (true) {      //Nested loops necessary because of nature of double puff

			if (analogRead(_pinNum) < _nomPressure - deadZone)
				_state = 0;    //Read in value of sip switch
			else
				_state = 1;

			//Detect start of puff. If first sip, start debounce timer. If second sip, store time of first sip and reset debounce timer
			if (_state == LOW && _lastState == HIGH) {
				if (count)  //Second sip detected, store time of first puff
					puffTime = _lastBounceTime;

				_lastBounceTime = millis();
				delay(5);  //Delay to allow switch to catch up
			}

			//Detect end of puff. If first, increment count. If second, determine spacing time of puffs and iterate external loop.
			if (_state == HIGH && _lastState == LOW && (millis() - _lastBounceTime > debounceTime)) {
				if (count) {     //Detect second puff
					puffTime = millis() - puffTime;
					count = false;  //Reset count for next round
					calCount++;   //Increment counter for external loop
					doubleSipAvg += puffTime;  //Add to average
					o_Serial.print("+3\n");

					ConfirmResponse(o_Serial, "+3\n");

					break;    //Break out of internal loop
				}
				count = true;  //If first sip, set count to true. Second sip won't make it this far.
			}

			_lastState = _state;  //Record old value of switch to compare next iteration
		}

		_lastState = 1;  //Reset for next round
	}








	o_Serial.print("c5\n");


	ConfirmResponse(o_Serial, "c5\n");

	//calCount = 0;
	//bool switchpuff =true;
	////Sip Puff sequance
	//// - Tests the average length of a double sip to determine the appropriate double sip threshold
	//while (calCount < 3) {
	   // while (true) {      //Nested loops necessary because of nature of double puff

		  //  if (analogRead(_pinNum) < _nomPressure - deadZone)
		  //	  _state = 0;    //Read in value of sip switch
		  //  else
		  //	  _state = 1;

		  //  //Detect start of puff. If first sip, start debounce timer. If second sip, store time of first sip and reset debounce timer
		  //  if (_state == LOW && _lastState == HIGH) {
		  //	  if (count)  //Second sip detected, store time of first puff
		  //		  puffTime = _lastBounceTime;

		  //	  _lastBounceTime = millis();
		  //	  delay(5);  //Delay to allow switch to catch up
		  //  }

		  //  //Detect end of puff. If first, increment count. If second, determine spacing time of puffs and iterate external loop.
		  //  if (_state == HIGH && _lastState == LOW && (millis() - _lastBounceTime > debounceTime)) {

		  //	  switchpuff = true;
		  //	  while (switchpuff) {


		  //		  _lastState = 1;
		  //		  calCount = 0;


		  //		  _state = 0;
		  //		  _lastState = 0;

		  //		  _lastBounceTime = 0;
		  //		   

		  //		  if (analogRead(_pinNum) > _nomPressure + deadZone) {
		  //			  _state = 1;    //Read in value of sip switch

		  //		  }
		  //		  else
		  //			  _state = 0;

		  //		  //Detect puff, start debounce timer
		  //		  if (_state == 1 && _lastState == 0)
		  //			  _lastBounceTime = millis();

		  //		  //Detect puff End, check against debounce timer, increment if successful reading
		  //		  if (_state == 0 && _lastState == 1 && (millis() - _lastBounceTime > debounceTime)) {
		  //			  puffTime = millis() - _lastBounceTime;

		  //			  o_Serial.print("++1");
		  //			  o_Serial.flush();
		  //			  doubleSipAvg += puffTime;
		  //			  calCount++;
		  //			  switchpuff = false;
		  //		  }

		  //		  _lastState = _state;    //Record old value of switch to compare next iteration



















		  //	  }

		  //   
		  //	  //if (count) {     //Detect second puff
		  //		 // puffTime = millis() - puffTime;
		  //		 // count = false;  //Reset count for next round
		  //		 // calCount++;   //Increment counter for external loop
		  //		 // doubleSipAvg += puffTime;  //Add to average

		  //		 // o_Serial.print("+4");
		  //		 // break;    //Break out of internal loop
		  //	  //}
		  //	  //count = true;  //If first sip, set count to true. Second sip won't make it this far.
		  //  }

		  //  _lastState = _state;  //Record old value of switch to compare next iteration
	   // }

	   // _lastState = 1;  //Reset for next round
	//}






	doubleSipAvg = 0;
	calCount = 0;
	bool switchs = true;
	//Sip Puff sequance
	int multitime;
	// - Tests the average length of a double sip to determine the appropriate double sip threshold
	while (calCount < 3) {
		//Nested loops necessary because of nature of double puff

		if (analogRead(_pinNum) < _nomPressure - deadZone)
			_state = 0;    //Read in value of sip switch
		else
			_state = 1;

		//Detect start of puff. If first sip, start debounce timer. If second sip, store time of first sip and reset debounce timer
		if (_state == LOW && _lastState == HIGH)
			_lastBounceTime = millis();

		//Detect end of puff. If first, increment count. If second, determine spacing time of puffs and iterate external loop.
		if (_state == HIGH && _lastState == LOW && (millis() - _lastBounceTime > debounceTime)) {

			bool oneTime = true;

			multitime = millis();
			_state = 0;
			_lastState = 0;
			switchs = true;
			while (switchs) {









				if (analogRead(_pinNum) > _nomPressure + deadZone) {
					_state = 1;    //Read in value of sip switch

				}
				else
					_state = 0;

				//Detect puff, start debounce timer
				if (_state == 1 && _lastState == 0) {

					if (oneTime)
					{

						doubleSipAvg += (millis() - multitime);
						oneTime = false;
					}


					_lastBounceTime = millis();



				}
				//Detect puff End, check against debounce timer, increment if successful reading
				if (_state == 0 && _lastState == 1 && (millis() - _lastBounceTime > debounceTime)) {

					o_Serial.print("+4\n");
					ConfirmResponse(o_Serial, "+4\n");


					calCount++;
					switchs = false;
					_lastState = 1;
				}

				_lastState = _state;    //Record old value of switch to compare next iteration



















			}

			_lastState = 1;  //Reset for next round
			_state = 0;

			//if (count) {     //Detect second puff
			   // puffTime = millis() - puffTime;
			   // count = false;  //Reset count for next round
			   // calCount++;   //Increment counter for external loop
			   // doubleSipAvg += puffTime;  //Add to average

			   // o_Serial.print("+4");
			   // break;    //Break out of internal loop
			//}
			//count = true;  //If first sip, set count to true. Second sip won't make it this far.
		}

		_lastState = _state;
	}









	int avg = doubleSipAvg / 3;
	//String stats1 = "c6," + String(longStateTime) + "," + String(avg);
	//const char* stat2 = stats1+ "," + avg + "\n"

	o_Serial.flush();

	if (avg < longStateTime)
		multiStateTime = longStateTime + 200;           // time limit for multiple sip/puff inputs
	else if (avg > longStateTime && avg - longStateTime < 200)
		multiStateTime = longStateTime + 200;
	else
		multiStateTime = avg;

	longStateTime = longStateTime;
	//String stats = stats1 + "," + avg+"\n" ;

	String stats1 = "c6," + String(longStateTime) + "," + String(multiStateTime);
	o_Serial.print(stats1);
	ConfirmResponse(o_Serial, "c6,\n");

	//read if any rubish data stayed on the serial


	while (o_Serial.available() > 0) {
		o_Serial.readString();
	}
	o_Serial.flush();







}





