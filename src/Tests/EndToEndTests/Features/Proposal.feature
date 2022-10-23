Feature: Proposal

A short summary of the feature

@tag1
Scenario: Add proposal
	Given product with name "product11" and description "product11 description" is created
	Given seller with name "seller11" and description "seller11 description" is created
	Given proposal with description "proposal1 description" price 25 currency UAH is created
	When get proposal
	Then the result should be 200 description "proposal1 description" price 25 currency UAH
	Then delete proposal
	Then delete product
	Then delete seller
	
