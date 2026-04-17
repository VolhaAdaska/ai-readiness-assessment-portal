---
agent: 'agent'
description: Generate a project README for the practical assignment
---

Generate a professional README.md for the AI Readiness Assessment Portal repository.

Context:
This is a full-stack practical assignment project built with:
- ASP.NET Core Web API
- Clean/layered architecture
- EF Core with SQLite
- React + TypeScript + Vite
- GitHub Copilot customization via repository instructions, path-specific instructions, prompt files, and AGENTS.md

Goal:
Create a README that is suitable for repository review and practical assignment submission.

Requirements:
- keep the README professional, clear, and well-structured
- describe the product as a real MVP, not as a toy project
- explain what the application does
- explain the architecture briefly
- explain the main implemented features
- explain how to run backend and frontend locally
- explain database setup / migrations
- include a section about GitHub Copilot customization used in the repository
- include a section about prompt files and agent-based workflow
- include a section about tests
- include a section about current MVP scope and possible future improvements
- do not invent features that do not exist
- before applying changes, list the sections that will be included

Suggested README structure:
1. Project title
2. Overview
3. Features
4. Architecture
5. Tech stack
6. GitHub Copilot customization
7. Prompt files / agent workflow
8. How to run the backend
9. How to run the frontend
10. Database and migrations
11. Tests
12. MVP scope
13. Future improvements

Return:
1. planned README sections
2. then generate the README.md content