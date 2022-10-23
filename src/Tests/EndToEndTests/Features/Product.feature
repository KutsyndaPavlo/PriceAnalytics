Feature: Product

A short summary of the feature

Scenario: Add product
	Given product with name "product11" and description "product11 description" is created
	When get product
	Then the product get result should be 200 and name "product11" and description "product11 description"
	Then delete product

Scenario: Update product
    Given product with name "product1" and description "product1 description" is created
	Given product with name "product2" and description "product2 description" is updated
	When get product
	Then the product get result should be 200 and name "product2" and description "product2 description"
	Then delete product

Scenario: Delete product
    Given product with name "product3" and description "product3 description" is created
	Given delete product
	When get product
	Then the product delete result should be 404 after delete
