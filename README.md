# dev-challenge

## Setup and Run
The Angular app and backed are already deployed in Azure VM. Access the application from below link.
http://52.163.120.173/clientapp/index.html
The demo app initially loads 10 entities. After that, directly connected nodes and edges can be loaded by clicking a node. Nodes are displayed in different colors to reflect node types.

![screenshot of the gauge](https://github.com/sheldonyss/dev-challenge/blob/master/screenshot/chrome_2018-09-08_23-09-43.png?raw=true)

## Tech stack
Main consideration is the time constraint. So 
-	Asp.net core web API
-	SQL server
-	Swagger
-	Angular 6
-	Vis.js

## What has been done
1. Asp.net Core web API
  - Querying entity nodes with pagination
  - For a given node, explore its connected node.
2. SQL Database for storing nodes and edges
3. Angular client APP with vis.js for visualization

## What need to be improved
  - Backend API does not explore all edge type
  - Performance improvement (use Graph database)
