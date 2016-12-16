

# Band Tracker

### by Ryan Mathisen 12/16/2016

## Description

This project is a SQL exercise with C#.

## Installation
To Install this Program:
  * In SQL Server Management Studio (instructions may vary for SQLCMD users):
    * Connect to:
      * Service Type: Database Engine
      * Server Name: (localdb)\mssqllocaldb
      * Authentication: Windows Authentication
    * Select _'New Query'_
    * Enter the following Query into the query window:
      * CREATE DATABASE band_tracker;
    * Click _'Execute'_
    * Then enter the following Query into the query window:
      * USE band_tracker;
      * CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(100));
      * CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(100));
      * CREATE TABLE bands_venues (id INT IDENTITY(1,1), band_id INT, venue_id INT);
    * Click _'Execute'_ again
  * In Windows Powershell:
    * Clone this repository to the desired location on your computer
    * Run the command _"dnu restore"_
    * Run the command _"dnx kestrel"_
  * In your favorite internet browser:
    * Access the url "localhost:5004"
  * Enjoy!

To Run the Unit Tests for this Program (after following the above instructions):
  * in SQL Server Management Studio
    * _Right Click_ band_tracker > _Tasks > Backup_ in the Object Explorer
    * Click _Ok!_
    * _Right Click_ band_tracker > _Tasks > Restore > Database_ in the Object Explorer
    * Rename the database to band_tracker_test in the Destination section
    * Click _Ok!_
  * In Windows Powershell:
    * Run the command _"dnx test"_

I have also included the SQL scripts for this project (located in /SQL Scripts), which you can import in SSMS by doing the following:
  * Select _File > Open > File_ and select the .sql File
  * If you haven't already created the database, add the following code to the top of the script file:
      * CREATE DATABASE band_tracker (or band_tracker_test if you are using the test schema)
      * GO
  * Click _'Execute'_ and enjoy!

## Specifications

| Behavior                                                                     |
|------------------------------------------------------------------------------|
| User can create a Venue                                                      |
| User can show a single Venue                                                 |
| User can show all Venues                                                     |
| User can update the details for each Venue                                   |
| User can delete each Venue                                                   |
| User can create a Band                                                       |
| User can show a single Band                                                  |
| User can show all Bands                                                      |
| User can add a Band to a Venue                                               |
| User can add a Venue to a Band                                               |
| User viewing a Venue can see all Bands that have played at that Venue so far |
| User viewing a Band can see all Venues that Band has played so far           |


## Technologies Used
* HTML5/CSS (using Bootstrap)
* C# (using Nancy/Razor)
* SQL (using SQL Server Management Studio and ADO.Net)

## License
Copyright (c) 2016 Ryan Mathisen

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
