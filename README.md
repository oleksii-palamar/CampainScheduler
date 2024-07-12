Write a microservice using .NET that sends scheduled campaigns for marketers. Campaign combine: 

1\. Template (it was provided you in templates folder. 

2\. Conditions on the list of customers. For example, all the males above 30 years. (for our exercise each campaign can handle one condition) 

3\. Time to send the campaign. 

4\. priority 

For example, if you are the marketer, schedule 5 campaigns for today. 

\***Pay attention** - template can be sent more that once, 

but each customer must receive just **one** campaign per day (depends on his priority). 

1\. Campaign 1 

a. Template A 

b. To all the Male customers 

c. At 10:15 AM 

d. Priority 1 

2\. Campaign 2 

a. Template B 

b. To all customers above the age 45 

c. At 10:05 AM 

d. Priority 2 

3\. Campaign 3 

a. Template C 

b. To all the customers from New York 

c. At 10:10 AM 

d. Priority 5 

4\. Campaign 4 

a. Template A 

b. To all the customers that deposit more than 100 $ 

c. At 10:15 AM 

d. Priority 3 

5\. Campaign 5 

a. Template C 

b. To all the customers that marked as new Customers 

c. At 10:05 AM 

d. Priority 4 

We want to see a system that can send these campaigns (for the assignment send them mean writing to the file called “sends” + specific date and wait 30 minutes) 

**Important Note:** 

● **Best Practices and Clean Code:** Ensure that your code follows best practices and clean code principles. 

● **Test Coverage:** Make sure to include comprehensive test coverage. 

● **Code Efficiency:** Consider the efficiency of your code, especially in handling large amounts of data.
