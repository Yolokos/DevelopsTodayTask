# DevelopsTodayTask

This console application processes and analyzes trip data from a CSV file. The program allows users to interactively query the database for specific trip-related information, such as the highest average tips, longest trips by distance or duration, and trips within a specific location and time range.

# Features

- Find the PULocationId with the highest average tip: Calculates and displays the location ID (PULocationId) with the highest average tip value.
- Find the 100 longest trips by distance: Retrieves and displays the 100 longest trips based on trip distance.
- Find the 100 longest trips by duration: Retrieves and displays the 100 longest trips based on time duration between pickup and dropoff.
- Search trips by PULocationId and date range: Allows users to search for trips within a specific location (PULocationId) and a defined time range (start date and end date).

# Setup

1. Clone the repository: git clone [https://github.com/Yolokos/DevelopsTodayTask.git](https://github.com/Yolokos/DevelopsTodayTask.git)
2. Install dependencies
3. Configure the database:
    The program uses SQLite as the default database, but you can configure it to use other database systems.
    Make sure the database schema and tables are properly set up, including necessary indexes for performance optimization.
4. Run the application: dotnet run

# Deliverables

1. In the folder Scripts you can create Trip table by your own.
2. Total cound of rows: 29889
3. Assume your program will be used on much larger data files. Describe in a few sentences what you would change if you knew it would be used for a 10GB CSV input file.
   - Use IAsyncEnumerable for "dynamic reading"
   - Use Non-cluster index for fast search, filter etc.
