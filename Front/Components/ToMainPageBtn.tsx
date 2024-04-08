'use client'

export default function ToMainPageBtn() {
    return (
        <div>
            <button
                className="p-2 m-1 text-white bg-fuchsia-500 rounded-xl font-semibold text-xl"
                onClick={e => {
                    window.location.href = "http://localhost:3000"
                }}
            >
                To main page
            </button>
        </div>
    )
}