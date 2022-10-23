@Integration
Feature: Health Check

Scenario: 01 Basic Health Check
	When the Health Check end point is called
	Then an 'OK' status code is returned