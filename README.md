# PRG282_Project
## Project Overview
A Windows Forms system built to manage wild animal rehabilitation records at the Baobab Ridge Wildlife Rehabilitation Centre. The application allows staff to track animal progress, automatically classify status and housing units based on recovery scores, update and delete records, and generate management summary reports using plain text file storage.

## Group Members
- Daniel Ben Danilowitz (605150)
- Pranav Yash Khemlall (604806)
- Christian Swanepoel (604146)
- Keaan Leon van Niekerk (604496)
  
## Features
- Viewing all animal record contained in the system through the data view grid
- Add a new animal record to the file through a dedicated button along with associated textboxes
- Update the field of any animal record by selecting that record, either through the data grid view or by searching for its ID
- Delete an animal record by selecting it through data grid view and pressing the dedicated delete button
- Generate a summary report based upon calculations of the data within the animal.txt file

## File Formats

1. animals.txt
Stores animal records in UTF-8 encoding. Each valid record is on a single line separated by (`|`):

- ID|Name|Species|Age|Score|Status|Housing Unit
- 
Example:
- WR-0001|Tara|Leopard|2|70|Recovering|Outdoor Enclosure
- WR-0002|Harry|Tortoise|12|55|Stable|Recovery Ward
- 
2. summary.txt
Generated each time the summary report is run. Overwrites the file with the following format:

BAOBAB RIDGE WILDLIFE REHABILITATION CENTRE
ANIMAL SUMMARY REPORT
Generated: YYYY-MM-DD HH:mm
----------------------------------------
Total animals: 8
Average age (years): 10.75
Average recovery score: 51.50
----------------------------------------
Animals per status:
 Critical: 2
 Serious: 1
 Stable: 1
 Recovering: 2
 Release-Ready: 2
  
## Limitations
- The system does not allow for "native" editing of record data by double-clicking or pressing enter on any cell with the data grid view. All data enter operation have to be done through the dedicated textboxes.
- The system currently only reads from a txt file to provide data to the system. Migrating to another data source would be challenging as the code to not structured to smoothly transition other reading and writing operations.
- The deep of the system has not been completely tested. Individual code block have to tested through the use of sample data and breakpoints but unknown issue still exist within the program.
- Certain querying operations within the Update class are not optimized for best performance. Further refactoring would lead to future performance improvements.
