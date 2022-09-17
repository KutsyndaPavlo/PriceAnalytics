Feature: seller

A short summary of the feature

Scenario: Add seller
	Given seller with name "seller11" and description "seller11 description" is created
	When get seller
	Then the seller get result should be 200 and name "seller11" and description "seller11 description"
	Then delete seller

Scenario: Update seller
    Given seller with name "seller1" and description "seller1 description" is created
	Given seller with name "seller2" and description "seller2 description" is updated
	When get seller
	Then the seller get result should be 200 and name "seller2" and description "seller2 description"
	Then delete seller

Scenario: Delete seller
    Given seller with name "seller3" and description "seller3 description" is created
	Given delete seller
	When get seller
	Then the seller delete result should be 404 after delete
