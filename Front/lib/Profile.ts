import UserProfileToAddDto from "@/lib/Types/UserProfile/UserProfileToAddDto";

export function CreateProfile(profile: UserProfileToAddDto) {
    const fileByBytes = blobToByteArray(profile.Avatar);
    
    
}