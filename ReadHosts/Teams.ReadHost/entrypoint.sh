#!/bin/bash

set -e
run_cmd="dotnet run --server.urls http://*:5500"
exec $run_cmd