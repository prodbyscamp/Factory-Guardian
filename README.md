# FactoryGuardian 개발 참고 자료 및 기술 적용 내역
## 1. WPF MVVM(Model-View-ViewModel) 구조 설계

FactoryGuardian은 WPF 기반 데스크톱 애플리케이션으로 개발되었으며, 화면(View), 화면 제어(ViewModel), 데이터(Model)를 분리하는 MVVM 패턴을 적용하였다.

MVVM 패턴을 적용함으로써 UI 코드와 비즈니스 로직을 분리하고 유지보수성과 확장성을 높였다.

참고 자료:

Microsoft - WPF 데이터 바인딩 개요
https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/

Microsoft - MVVM 패턴 설명
https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm

적용 내용:

Views
Dashboard
EquipmentList
EquipmentDetail
ViewModels
DashboardViewModel
EquipmentViewModel
DetailViewModel
Models
Equipment
MaintenanceTask
Alarm

구조:

View
 ↓ Binding
ViewModel
 ↓
Service
 ↓
Model
 ↓
Database

## 2. SQLite 데이터베이스 적용

설비 데이터 저장을 위해 SQLite Embedded Database를 사용하였다.

별도의 DB 서버 설치 없이 단일 파일 기반으로 동작하며, 현장 설비 관리 프로그램이나 Edge 환경에서 활용하기 적합하다.

참고 자료:

Microsoft.Data.Sqlite 공식 문서

https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/

NuGet Package:

https://www.nuget.org/packages/Microsoft.Data.Sqlite/

적용 내용:

데이터베이스 구조:

FactoryGuardian.db

Equipment
 ├ 설비 정보
 ├ 센서 데이터
 └ 분석 결과

MaintenanceTask
 └ 유지보수 작업

Alarm
 └ 이상 상태 기록

주요 적용 기술:

SQLite Connection 관리
Table 자동 생성
Foreign Key 관계 설정
CRUD 기반 Repository 구조

## 3. 데이터 접근 계층 Repository Pattern 적용

DB 접근 코드를 Service와 분리하기 위해 Repository 구조를 적용하였다.

참고 자료:

Microsoft - Repository Pattern 설명

https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application

적용 구조:

ViewModel

   ↓

Service

   ↓

Repository

   ↓

SQLite

예:

EquipmentRepository

- GetAll()
- GetById()
- Insert()
- Update()
- Delete()

효과:

데이터베이스 변경 시 영향 최소화
테스트 용이성 증가
유지보수성 향상

## 4. 설비 상태 Health Score 알고리즘

설비 상태 분석을 위해 센서 데이터를 기반으로 Health Score를 계산하는 알고리즘을 구현하였다.

참고 자료:

IBM Predictive Maintenance 개념 자료

https://www.ibm.com/topics/predictive-maintenance

AWS Predictive Maintenance 소개

https://aws.amazon.com/predictive-maintenance/

적용 개념:

입력 데이터:

Temperature
Vibration
Running Hours
Inspection Delay

계산 결과:

Health Score

100
 |
 |
50
 |
 |
0

낮을수록 설비 상태 불량

예:

온도 상승
+
진동 증가
+
운전시간 증가

↓

Health Score 감소

↓

Risk Score 증가

↓

Maintenance Priority 상승

## 5. Risk Score 위험도 분석

설비 위험도를 수치화하기 위해 Risk Score 계산 로직을 구현하였다.

참고 자료:

ISO 55000 Asset Management 개념

https://www.iso.org/standard/55088.html

적용 방식:

Risk Score 계산 요소:

Risk Score

=
Health 상태
+
현재 온도
+
최근 고장 횟수

결과:

0 ~ 30
Low

30 ~ 60
Medium

60 ~ 80
High

80 이상
Critical
6. 유지보수 우선순위 계산

예방 정비 대상 설비를 자동 선정하기 위해 Priority Score 알고리즘을 적용하였다.

참고 자료:

Reliability Centered Maintenance(RCM)

https://www.plantengineering.com/articles/reliability-centered-maintenance/

적용 방식:

Priority Score

=
운전시간 비율 × 30%

+
Risk Score × 40%

+
점검 지연 × 30%

결과:

Critical

↓

High

↓

Medium

↓

Low

우선순위가 높은 설비부터 유지보수를 수행하도록 설계하였다.

7. WPF UI 데이터 바인딩

설비 상태가 변경될 때 화면을 자동 업데이트하기 위해 INotifyPropertyChanged 패턴을 적용하였다.

참고 자료:

Microsoft - INotifyPropertyChanged Interface

https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged

적용 내용:

Equipment Model

값 변경

↓

PropertyChanged 발생

↓

View 자동 업데이트
