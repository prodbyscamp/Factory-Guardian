using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace FactoryGuardian.Database;


public class DbInitializer
{
    private readonly string _connectionString;


    public DbInitializer()
    {
        // 실행 파일 위치에 DB 파일 생성
        var path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "FactoryGuardian.db");

        _connectionString = $"Data Source={path}";
    }


    // 테이블이 존재하지 않을 경우 생성한다.
    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        EnableForeignKeys(connection);

        CreateEquipmentTable(connection);
        CreateMaintenanceTable(connection);
        CreateInspectionTable(connection);
        CreateAlarmTable(connection);
        CreateFailureHistoryTable(connection);
    }


    private void EnableForeignKeys(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
            "PRAGMA foreign_keys = ON;";

        command.ExecuteNonQuery();
    }


    // 설비 정보 & 센서 상태 데이터 저장 테이블
    private void CreateEquipmentTable(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
        """
        CREATE TABLE IF NOT EXISTS Equipment
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,

            Name TEXT NOT NULL,
            ModelName TEXT,
            Manufacturer TEXT,
            InstallDate TEXT,
            Location TEXT,

            Temperature REAL DEFAULT 0,
            Vibration REAL DEFAULT 0,
            Power REAL DEFAULT 0,
            RunningHours REAL DEFAULT 0,

            HealthScore REAL DEFAULT 100,
            RiskScore REAL DEFAULT 0,

            RiskLevel TEXT DEFAULT 'Low',

            PriorityScore REAL DEFAULT 0,
            PriorityLevel TEXT DEFAULT 'Low',

            Status TEXT DEFAULT 'Idle',

            LastInspectionDate TEXT,
            NextInspectionDate TEXT,

            CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP,
            UpdatedAt TEXT DEFAULT CURRENT_TIMESTAMP
        );
        """;

        command.ExecuteNonQuery();
    }


    // 유지&보수 작업 이력을 관리 테이블
    private void CreateMaintenanceTable(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
        """
        CREATE TABLE IF NOT EXISTS MaintenanceTask
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,

            EquipmentId INTEGER NOT NULL,

            Title TEXT NOT NULL,
            Description TEXT,

            TaskType TEXT DEFAULT 'Preventive',
            Priority TEXT DEFAULT 'Medium',
            Status TEXT DEFAULT 'Created',

            AssignedTo TEXT,

            CreatedDate TEXT DEFAULT CURRENT_TIMESTAMP,
            StartedDate TEXT,
            CompletedDate TEXT,

            Notes TEXT,

            FOREIGN KEY(EquipmentId)
            REFERENCES Equipment(Id)
        );
        """;

        command.ExecuteNonQuery();
    }


    // 설비 점검 기록을 저장 테이블
    private void CreateInspectionTable(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
        """
        CREATE TABLE IF NOT EXISTS Inspection
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,

            EquipmentId INTEGER NOT NULL,

            InspectorName TEXT NOT NULL,

            InspectionDate TEXT DEFAULT CURRENT_TIMESTAMP,

            Result TEXT DEFAULT 'Normal',

            Notes TEXT,

            FOREIGN KEY(EquipmentId)
            REFERENCES Equipment(Id)
        );
        """;

        command.ExecuteNonQuery();
    }


    // 센서 이상 & 위험 상태 알림 노트
    private void CreateAlarmTable(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
        """
        CREATE TABLE IF NOT EXISTS Alarm
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,

            EquipmentId INTEGER NOT NULL,

            Message TEXT NOT NULL,

            AlarmLevel TEXT DEFAULT 'Info',

            IsAcknowledged INTEGER DEFAULT 0,

            CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP,

            FOREIGN KEY(EquipmentId)
            REFERENCES Equipment(Id)
        );
        """;

        command.ExecuteNonQuery();
    }


   
    // FailureAnalyzer에서 통계 계산 시 사용
    private void CreateFailureHistoryTable(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
        """
        CREATE TABLE IF NOT EXISTS FailureHistory
        (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,

            EquipmentId INTEGER NOT NULL,

            FailureDate TEXT DEFAULT CURRENT_TIMESTAMP,

            FailureType TEXT,

            Description TEXT,

            RepairDate TEXT,

            DowntimeHours REAL DEFAULT 0,

            FOREIGN KEY(EquipmentId)
            REFERENCES Equipment(Id)
        );
        """;

        command.ExecuteNonQuery();
    }
}