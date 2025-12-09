# MovieRental Exercise
• The app is throwing an error when we start, please help us. Also, tell us what caused the issue.
A: The issue was in the application startup — the Singleton service had Entity Framework (scoped) as a sub-dependency. There are some combinations between the three lifetimes that .NET warns us about.

• The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?
A: Yes.
The difference is in how the thread is used: when the method is not async, the thread remains fully occupied until the operation completes. In the async version, we allow the thread to be released while waiting for the external operation, making it available for other tasks.

• Please finish the method to filter rentals by customer name, and add the new endpoint.
A: Yes. Done.

• We noticed we do not have a table for customers; it is not good to have just the customer name in the rental.
Can you help us add a new entity for this? Don’t forget to change the customer name field to a foreign key, and fix your previous method!
A: Yes. Done.

• In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.
A: It is considered a not-very-common practice. Points for improvement: filtering, pagination, data limits, and other optimizations.

• No exceptions are being caught in this API, how would you deal with these exceptions?
A: In my view, exception handling must be used carefully.
I use try–catch only to protect calls to external or uncontrolled code (e.g., external API, timeout, database).


	## Challenge (Nice to have)
We need to implement a new feature in the system that supports automatic payment processing. Given the advancements in technology, it is essential to integrate multiple payment providers into our system.

Here are the specific instructions for this implementation:

* Payment Provider Classes:
    * In the "PaymentProvider" folder, you will find two classes that contain basic (dummy) implementations of payment providers. These can be used as a starting point for your work.
* RentalFeatures Class:
    * Within the RentalFeatures class, you are required to implement the payment processing functionality.
* Payment Provider Designation:
    * The specific payment provider to be used in a rental is specified in the Rental model under the attribute named "PaymentMethod".
* Extensibility:
    * The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.
* Payment Failure Handling:
    * If the payment method fails during the transaction, the system should prevent the creation of the rental record. In such cases, no rental should be saved to the database.
