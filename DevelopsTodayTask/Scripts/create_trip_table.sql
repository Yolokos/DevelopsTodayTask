CREATE TABLE Trips (
    Id INTEGER NOT NULL CONSTRAINT "PK_Trips" PRIMARY KEY AUTOINCREMENT,
    TpepPickupDatetime DATETIME NOT NULL,
    TpepDropoffDatetime DATETIME NOT NULL,
    PassengerCount INTEGER NOT NULL,
    TripDistance REAL NOT NULL,
    StoreAndFwdFlag TEXT NOT NULL,
    PULocationID INTEGER NOT NULL,
    DOLocationID INTEGER NOT NULL,
    FareAmount REAL NOT NULL,
    TipAmount REAL NOT NULL
);

CREATE UNIQUE INDEX idx_unique_trip_times 
ON TripsTest (TpepPickupDatetime, TpepDropoffDatetime);

CREATE INDEX idx_pulocation_id
ON TripsTest (PULocationID);

CREATE INDEX idx_dolocation_id
ON TripsTest (DOLocationID);

CREATE INDEX idx_pickup_datetime
ON TripsTest (TpepPickupDatetime);

CREATE INDEX idx_dropoff_datetime
ON TripsTest (TpepDropoffDatetime);

CREATE INDEX idx_trip_distance
ON TripsTest (TripDistance);

CREATE INDEX idx_tip_amount
ON TripsTest (TipAmount);