#Scenario: Add two numbers
#	Given I have entered 50 into the calculator
#	And I have entered 70 into the calculator
#	When I press add
#	Then the result should be 120 on the screen

Feature: Adding notes that are visible to site visitors
	I can publish a note that is later visible to 
	site visitors

Scenario: Adding note
	Given I am loged in as 'automatyzacja' with password 'jesien2018'
	And I add a note with title 'abc' and content 'this is spec flow FTW!!! :D'
	When I publish a note 
	And logout
	And I open new note link
	Then I am logged out
	And I should be able to view the title 'abc' and content 'this is spec flow FTW!!! :D'