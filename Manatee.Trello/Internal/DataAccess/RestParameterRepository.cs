using System;
using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.DataAccess
{
	internal static class RestParameterRepository
	{
		private static readonly Dictionary<Type, string> _fieldSets;

		static RestParameterRepository()
		{
			_fieldSets = new Dictionary<Type, string>
				{
					// I know we don't need all of the types here,
					// but we'll include them anyway
					// because typing long lists of data is fun.
					{typeof(IJsonAction), "idMemberCreator,data,type,date"},
					{typeof(IJsonActionData), "attachment,board,boardSource,boardTarget,card,checkItem,checkList,dateLastEdited,list,listAfter,listBefore,member,idMember,idMemberAdded,old,org,organization,plugin,text,value"},
					{typeof(IJsonActionOldData), "desc,list,text,pos,closed"},
					{typeof(IJsonAttachment), "bytes,date,idMember,isUpload,mimeType,name,previous,url"},
					{typeof(IJsonBadges), "votes,viewingMemberVoted,subscribed,fogbugz,due,dueComplete,description,comments,checkItemsChecked,checkItems,attachments"},
					{typeof(IJsonBoard), "name,desc,closed,idOrganization,prefs,url,subscribed"},
					{typeof(IJsonBoardBackground), "background,backgroundColor,backgroundImage,backgroundImageScaled,backgroundTile"},
					{typeof(IJsonBoardMembership), "idMember,memberType,deactivated"},
					{typeof(IJsonBoardPersonalPreferences), "showSidebar,showSidebarMembers,showSidebarActions,showSidebarActivity,showListGuide,emailPosition,idEmailList"},
					{typeof(IJsonBoardPreferences), "permissionLevel,voting,comments,invitations,selfJoin,cardCovers,calendarFeed,cardAging"},
					{typeof(IJsonBoardVisibilityRestrict), "public,org,private"},
					{typeof(IJsonCard), "badges,closed,dateLastActivity,desc,due,idBoard,idList,idShort,idAttachmentCover,manualCoverAttachment,labels,name,pos,shortUrl,url"},
					{typeof(IJsonCheckItem), "state,name,pos"},
					{typeof(IJsonCheckList), "name,idBoard,idCard,checkItems,pos"},
					{typeof(IJsonImagePreview), "width,height,url,scaled"},
					{typeof(IJsonLabel), "idBoard,color,name,uses"},
					{typeof(IJsonList), "name,closed,idBoard,pos,susbcribed"},
					{typeof(IJsonMember), "avatarHash,bio,fullName,initials,memberType,status,url,username,avatarSource,confirmed,email,gravatarHash,loginTypes,trophies,uploadedAvatarHash,oneTimeMessagesDismissed,similarity,prefs"},
					{typeof(IJsonMemberPreferences), "minutesBetweenSummaries,colorBlind"},
					{typeof(IJsonMemberSearch), ""}, // this model is only sent
					{typeof(IJsonMemberSession), "isCurrent,isRecent,dateCreated,dateExpires,dateLastUsed,ipAddress,type,userAgent"},
					{typeof(IJsonNotification), "idMemberCreator,data,unread,type,date"},
					{typeof(IJsonNotificationData), "attachment,board,boardSource,boardTarget,card,checkItem,checklist,list,listAfter,listBefore,member,idMember,old,org,text"},
					{typeof(IJsonNotificationOldData), "desc,list,pos,text,closed"},
					{typeof(IJsonOrganization), "name,displayName,desc,descData,url,website,logoHash,products,powerUps,prefs"},
					{typeof(IJsonOrganizationMembership), "idMember,memberType,unconfirmed"},
					{typeof(IJsonOrganizationPreferences), "permissionLevel,externalMembersDisabled,associatedDomain,boardVisibilityRestrict"},
					{typeof(IJsonParameter), "value"},
					{typeof(IJsonPosition), ""},
					//{typeof(IJsonPowerUp), ""},		 // not including this b/c implementations may rely on additional data
					//{typeof(IJsonPowerUpData), ""},    // not including this b/c implementations may rely on additional data
					{typeof(IJsonSearch), "actions,board,cards,members, organizations"},
					{typeof(IJsonSticker), "left,image,imageScaled,rotate,top,imageUrl,zIndex"},
					{typeof(IJsonToken), "identifier,idMember,dateCreated,dateExpires,permissions"},
					{typeof(IJsonTokenPermission), "idModel,modelType,read,write"},
					{typeof(IJsonWebhook), "description,idModel,callbackURL,active"},
					{typeof(IJsonWebhookNotification), "action"},
				};
		}

		public static Dictionary<string, string> GetParameters<T>()
		{
			var type = typeof(T);
			if (type == typeof(Me))
				type = typeof(Member);
			string fields;
			return _fieldSets.TryGetValue(type, out fields)
				       ? new Dictionary<string, string> {{"fields", _fieldSets[type]}}
				       : new Dictionary<string, string>();
		}
	}
}