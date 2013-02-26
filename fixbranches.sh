#!/bin/sh

# Remove all branches except master, create a branch using the name of each commit
git reset .
git clean -df .
git checkout master

git branch | grep -v master | xargs -n1 git branch -D
git log --pretty='format:%s %h' | xargs -n1 -I\{} echo git branch \{} | sh

git branch working Initial
