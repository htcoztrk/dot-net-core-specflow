Feature: Calculator
@Deneme
Scenario: Demo

	* Wake up the driver
	* Go to https://www.kitapyurdu.com/index.php?route=account/login Url
	* Wait 3 seconds

@Uzun
Scenario: Uzun
	* Wake up the driver
	* Go to https://www.kitapyurdu.com/index.php?route=account/login Url
	* Wait 3 seconds
	* Go to https://www.amazon.com.tr/
	* Wait 2 seconds
	* Go to https://www.trendyol.com/
	* Wait 2 seconds
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