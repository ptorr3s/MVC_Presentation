@echo off
git reset .
git checkout .
git clean -df .
git checkout %1
