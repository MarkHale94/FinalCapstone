# PharmaQueue

----

## What is PharmaQueue?
> PharmaQueue is an app designed by me to solve a common problem that I see occur in the retail pharmacy setting. As a pharmacy technician, I have seen many customers come to the store to pick up medicine, but it turns out to not be ready yet. This causes a great deal of inconvenience for both the customer and the workers. The customer is upset because their medications aren't ready and the employees are upset because they must now scramble to rearrange the priority of their tasks in order to get the customer on their way as soon as possible. What PharmaQueue aims to solve is this stress-point by allowing customers to see where in the work queue their prescriptions are. This way, the customer knows exactly when their prescriptions are completed, removing a lot of the guess work and the inconvenience of calling to check on the status as well.

----

## What Was Used to Create PharmaQueue?
* ASP.NET
* Entity Framework
* Identity Framework
* SQL Server

----

## How Do I Get Started?

### These are the steps you'll need in order to install PharmaQueue on your local machine. 
> Note: all commands will need to be input without the quotations. The quotations are only for easier visibility unless otherwise stated. The only employee account made during migrations has the log in of "admin@admin.com" with the password "Admin8*". All newly registered accounts are automatically converted into customer accounts, so if you would like to add in any addition employee accounts, you will need to manually update them.

* First clone down the repo to your local machine.
* Second, create an appsettings.json file inside the PharmaQueue folder and add the correct connection string for your machine.
* Third, open the package manager console and run "Add-Migration Initial"
* Next, save the changes that you made by running in the package manager console "Update-Database"
* Start up the application and you are good to go.

----

## Entity Relationship Diagram
![ERD](https://i.imgur.com/N7gkjVu.png)