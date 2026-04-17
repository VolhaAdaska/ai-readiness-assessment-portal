---
agent: 'agent'
description: Upgrade frontend visual style without changing logic
---

Upgrade the frontend visual style of the AI Readiness Assessment Portal to look more modern, clean, and professional while keeping all existing logic unchanged.

Important:
- styling/UI changes only
- do NOT change business logic
- do NOT change API integration
- do NOT change routing
- do NOT change application flow
- do NOT remove any existing functionality
- do NOT introduce new dependencies

The current UI works but looks too basic and plain.
The goal is to make it look like a modern internal SaaS / assessment platform.

Use Tailwind CSS and improve the visual design of:
- app background
- top navigation
- dashboard layout
- assessment cards
- progress bars
- status badges
- labels / typography
- action buttons
- empty or fallback states
- forms and containers where applicable

Design direction:
- clean light background (e.g. subtle gray or slate tint)
- white cards with rounded corners and soft shadows
- stronger visual hierarchy
- modern blue accent color
- improved spacing and consistent section layout
- more polished buttons with hover states
- better looking status badges for Completed / In Progress
- improved typography for card title, labels, and secondary values
- make the app feel more intentional and visually structured

Examples of improvements:
- add container max-width and better spacing between sections
- use larger page headings and cleaner alignment
- style nav links with active state and hover state
- make assessment cards visually balanced with better internal spacing
- improve progress bar appearance with rounded lines and better percentage positioning
- use muted label text and stronger values
- use clearly styled badges
- make buttons more visually attractive and consistent
- make cards lift slightly on hover or have a subtle border/shadow treatment

Keep the current card content and logic intact:
- assessment title
- organization
- readiness level
- overall score
- categories completion
- status
- progress bar
- Continue Assessment / View Summary actions

Also improve the styling of other pages/components if they share the same design system, but do not alter behavior.

Before applying changes:
1. list files to create or update
2. explain the styling changes that will be made
3. then apply the changes

After applying:
- summarize the visual improvements
- confirm that functionality remains unchanged