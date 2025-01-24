CREATE TABLE Trips
(
    Id INT PRIMARY KEY IDENTITY,
    TpepPickupDatetime DATETIME,
    TpepDropoffDatetime DATETIME,
    PassengerCount INT,
    PULocationID INT,
    DOLocationID INT,
    TripDistance DECIMAL(18, 2),
    TipAmount DECIMAL(18, 2)
);

CREATE INDEX IX_Trips_PickupDropoffPassenger
ON Trips (TpepPickupDatetime, TpepDropoffDatetime, PassengerCount);

CREATE INDEX idx_pulocation_id
ON Trips (PULocationID);

CREATE INDEX idx_dolocation_id
ON Trips (DOLocationID);

CREATE INDEX idx_pickup_datetime
ON Trips (TpepPickupDatetime);

CREATE INDEX idx_dropoff_datetime
ON Trips (TpepDropoffDatetime);

CREATE INDEX idx_trip_distance
ON Trips (TripDistance);

CREATE INDEX idx_tip_amount
ON Trips (TipAmount);