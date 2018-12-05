@WithBackground
Feature: UsingBackground
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers
Background: 
	Given I am logged in as admin

Scenario: Can post new note
	Given I open new post page
	And type in a title and message
	When I press publish
	Then its published
