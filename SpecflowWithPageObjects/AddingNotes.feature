
Feature: Adding notes that are visible to site visitors
	I can publish a note that is later visible to 
	site visitors

Scenario: Adding note
	Given I am loged in as 'automatyzacja' with password 'jesien2018'
	When I publish a note 
	And logout
	And I open new note link
	Then I should be able to view the note