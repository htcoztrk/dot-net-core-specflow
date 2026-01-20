Feature: Calculator
@Deneme
Scenario: Demo

	* Wake up the driver
	* Go to https://www.kitapyurdu.com/index.php?route=account/login Url
	* Wait 3 seconds

@ignore
@skipdeneme
Scenario: skipdeneme

	* Wake up the driver
	* deneme skip
	* Go to https://www.kitapyurdu.com/index.php?route=account/login Url
	* Wait 3 seconds




@FailDeneme
Scenario: FailDeneme
	* Wake up the driver
	* Go to https://www.kitapyurdu.com/index.php?route=account/login Url
    * Get deneme dictionary folders json
	* Wait 3 seconds