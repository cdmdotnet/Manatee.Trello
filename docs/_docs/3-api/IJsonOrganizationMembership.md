---
title: IJsonOrganizationMembership
category: API
order: 239
---

# IJsonOrganizationMembership

Defines the JSON structure for the OrganizationMembership object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IJsonOrganizationMembership

## Properties

### [IJsonMember](IJsonMember#ijsonmember) Member { get; set; }

Gets or sets the ID of the member.

### [OrganizationMembershipType](OrganizationMembershipType#organizationmembershiptype)? MemberType { get; set; }

Gets or sets the membership type.

### bool? Unconfirmed { get; set; }

Gets or sets whether the membership is unconfirmed.

