import { WebPartContext } from "@microsoft/sp-webpart-base";

export interface IImageCreatorProps {
  description: string;
  isDarkTheme: boolean;
  environmentMessage: string;
  hasTeamsContext: boolean;
  userDisplayName: string;
  wpContext: WebPartContext;
}
