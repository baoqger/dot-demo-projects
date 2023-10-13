Feature: Signin

@smoke
Scenario: Signin to bstackdemo website
Given I open bstackdemo homepage
And I click signin link
And I enter the login details
And I click login button
Then Profile Name should appear