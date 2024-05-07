import 'bootstrap/dist/css/bootstrap.min.css';

export function BootStrapComponent({children}) {
    return (
        <div className="bootstrap-component">
            {children}
        </div>
    );
}