import * as React from "react";
import styles from "./ImageCreator.module.scss";
import type { IImageCreatorProps } from "./IImageCreatorProps";
import { PrimaryButton, TextField, Image, Stack, Label } from "@fluentui/react";
import { HttpClient } from "@microsoft/sp-http";

const ImageCreator: React.FunctionComponent<IImageCreatorProps> = (props) => {
  const [imagePromptValue, setImagePromptValue] = React.useState("");
  const [imageSrc, setImageSrc] = React.useState("");
  const [revisedPrompt, setRevisedPrompt] = React.useState("");

  const _generateImageClicked = async () => {
    const response = await props.wpContext.httpClient.get(
      `http://localhost:5236/dalle3?imagePrompt=${imagePromptValue}`,
      HttpClient.configurations.v1
    );

    const responseJson = await response.json();

    setImageSrc(responseJson.url);
    setRevisedPrompt(responseJson.revisedPrompt);
  };

  const onChangeFirstTextFieldValue = React.useCallback(
    (
      event: React.FormEvent<HTMLInputElement | HTMLTextAreaElement>,
      newValue?: string
    ) => {
      setImagePromptValue(newValue || "");
    },
    []
  );

  return (
    <section className={`${styles.imageCreator}`}>
      <Stack tokens={{ childrenGap: 5 }}>
        <Stack horizontal tokens={{ childrenGap: 5 }}>
          <Stack.Item grow={3}>
            <TextField
              value={imagePromptValue}
              onChange={onChangeFirstTextFieldValue}
            />
          </Stack.Item>
          <PrimaryButton
            text="Generate Image"
            onClick={_generateImageClicked}
          />
        </Stack>
        <Label>{revisedPrompt}</Label>
        <Image src={imageSrc} />
      </Stack>
    </section>
  );
};

export default ImageCreator;
