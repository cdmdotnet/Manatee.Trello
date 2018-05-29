---
title: IJsonBoardMembership
category: API
order: 217
---

# IJsonBoardMembership

Defines the JSON structure for the BoardMembership object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonBoardMembership

## Properties

### bool? Deactivated { get; set; }

Gets or sets whether the membership is deactivated.

### [IJsonMember](IJsonMember#ijsonmember) Member { get; set; }

Gets or sets the ID of the member.

### [BoardMembershipType](BoardMembershipType#boardmembershiptype)? MemberType { get; set; }

Gets or sets the membership type.

