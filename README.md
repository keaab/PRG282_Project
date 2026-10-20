# PRG282_Project
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
## Limitations
- The system does not allow for "native" editing of record data by double-clicking or pressing enter on any cell with the data grid view. All data enter operation have to be done through the dedicated textboxes.
- The system currently only reads from a txt file to provide data to the system. Migrating to another data source would be challenging as the code to not structured to smoothly transition other reading and writing operations.
- The deep of the system has not been completely tested. Individual code block have to tested through the use of sample data and breakpoints but unknown issue still exist within the program.
- Certain querying operations within the Update class are not optimized for best performance. Further refactoring would lead to future performance improvements.
