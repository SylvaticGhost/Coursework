type MainHeadProps = {
    text: string;
}

export default function MainHead({ text }: MainHeadProps) {
    return(
        <h1 className="text-xl text-purple-600 font-semibold">{text}</h1>
    )
}