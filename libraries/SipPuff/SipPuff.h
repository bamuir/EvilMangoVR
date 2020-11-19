#ifndef SipPuff_H
#define SipPuff_H

#if (ARDUINO <  100)
#include <WProgram.h>
#else
#include <Arduino.h>
#endif
#include <CommandCenter.h>
 
#define SipPuffSpeedVal  .1  // the value must be between 0-1
class SipPuff
{
   public:
    SipPuff(int pin);    	//constructor declaration
    void update();						//primary update function
	void update(int val, bool reset = false);						//primary update function

	void updateTest();
	void updateT(int val);
	void UpdateC();
	void UpdateBeta();
	bool setNominalPressure();			//call to zero out pressure gauge
	void CommandRecogination(controllerConfigurations  &   i_controllerConfiguration,  InputDeviceMessage & i_inputDeviceMessageManager);
	void CommandRecoginationTest(controllerConfigurations& i_controllerConfiguration, HardwareSerial& o_Serial, InputDeviceMessage& i_inputDeviceMessageManager);
	void calibrate(HardwareSerial&  o_Serial);
	String finalPattern;
    boolean active;            // the currently debounced state 
    long debounceTime;
    long multiStateTime;
    long longStateTime;
	int pressurePower;
	int deadZone;
	float SipPower;
	void readSensor();
	void UpdateNormal();
	bool setNominalPressure(int val);
	bool c_done;


//	void JoyStick::CommandRecogination(controllerConfigurations  &   i_controllerConfiguration, HardwareSerial & o_Serial){
  private:
    int8_t _state;            // Current appearant button state
    int8_t _lastState;           // previous button reading
    String _inputPattern;             // Number of button clicks within multiclickTime milliseconds
	int    inputPattern;
    long _lastBounceTime;         // the last time the button input pin was toggled, due to noise or a press
	int _nomPressure;
	int _pinNum;
	int lastPressure;
	bool activeConutinesSignal;
	bool long_sip_or_long_singal;
	bool cp_sp_signal;
	int maxPressure;
	
};

#endif

